# Plan
## Basics
- The task is to make an ASP.NET MVC app where the user can input a string for it to be turned into "l33t sp34k".  
- The app should use an external API for translation.  
- All calls and results should be recorded for future reference.  
- The code should be extendable, so as other translations could be supported as well.  
- The project should be maintainable (not only by the original developer).  
- Use of Ajax and JQuery is encouraged where appropriate.  
- There should be unit tests present, as such dependencies should be manageable.  
## Decisions
After pondering my options and reading a lot I've come to these architectural decisons:
- I will use MVC with ViewModels and DTOs.
- [DTOs](https://www.reddit.com/r/dotnet/comments/12tumni/confusion_about_whether_dtos_having_validation/) and [ViewModels](https://www.reddit.com/r/dotnet/comments/bqgcg6/validation_in_model_or_viewmodel/) will have validation to some extent (like length, being required)
- For database I will Entity Framework and as my app isn't big nor complex I will use [DbContext in Controller](https://www.reddit.com/r/dotnet/comments/if1suh/should_i_call_dbcontexts_from_controller/?rdt=36014)
- I will use a Code-First approach for database creation
- Calls for translation will be done via POST as it's not idempotent (a new request is added to db each time, responses for same input can vary)

## Notable parts of the system
- Main functionality Controller
- View/s for the user to interact with
- Model/s for records in database
- ModelBuilder (mostly to seed the database for tests)
- DbContext to communicate the Controller with database
- Entity Framework Database to write calls and results
- TranslationClient to communicate with funtranslations API
- DTOs to store and transport data between components
- ViewModels to display and collect data for/from the user
- Unit tests
- Constants

## Order of development
If possible/neccessary each element will be built along with its unit tests.
1. TranslationClient/DTOs - communication with funtranslations API, returning results using DTO
2. TranslationController - API, routing, controls input, translates user input using TranslationClient to return JsonResults (at this point no views, model, database; in later stages controller will be extended to accommodate these)
3. Model/ModelBuilder/DbContext - storing calls and results in the database, dbcontext seeded on creation, adding to controller via DI
4. Controller interaction with db - adding adequate data to the database during calls
5. View/s and Controller - adding new methods to the Controller with corresponding views (they will use parts of code which were already tested)
6. Ajax/Controller/View - adding Ajax so as whole page doesn't have to reload to change translated text

## Plans for testing
Each of the points will refer to points from the previous section.
1. As for unit testing, unit tests will cover proper exceptions being thrown (checking incoming string); integration tests will cover whether ~~proper results are obtained~~ (as it turns out the results are, from the end user's perspective, non-deterministic, therefore we can only test format of json message and whether communication was successful)
2. Unit tests will use a stub/mock of TranslationClient, they will check the incoming string and the results (for example - whether the dictionary contains some specific keys)
3. As per [Microsoft's recommendation](https://learn.microsoft.com/en-us/ef/core/testing/), we will test against the [production database](https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database) (of course with a different connection string than the intended one), we will test whether CRUD works as intended, whether the restrictions work as intended, etc
4. Here we will use a stub/mock of TranslationClient and a [class fixture for our database](https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database#creating-seeding-and-managing-a-test-database), test used db operations
5-6 are generally harder for unit testing, possibly won't have unit tests
## Model
1. TranslationRequest:
	- int Id
	- string TextToTranslate - Required, Not null, Len<1-100>
	- DateTime RequestDate - Required
	- int TranslationId - Required (won't be required to be passed by the user)
	- Translation Translation
	- TranslationResponse? Response
2. TranslationResponse:
	- int Id
	- string TranslatedText - Required, Not null, Len<0-300>
	- int TranslationRequestId - Required
	- TranslationRequest TranslationRequest
3. Translation:
	- int Id
	- string Translation - Required, Unique, Not null, Len<1-50>
## DTOs
1. TranslationRequestDto:
	- int Id
	- string TextToTranslate - Required, Not null, Len<1-100>
	- DateTime RequestDate - Required
	- int TranslationId - Required
	- TranslationDto Translation
2. TranslationRequestCreateDto
	- int? Id
	- string TextToTranslate - Required, Not null, Len<1-100>
	- DateTime? RequestDate - (if not provided, controller will provide datatime)
	- int? TranslationId - default: 1(1 - leetspeak)(fot the sake of future updates)
3. TranslationResponseDto:
	- int Id
	- string TranslatedText - Required, Not null, Len<0-300>
	- int TranslationRequestId - Required
	- TranslationRequestDto TranslationRequest
4. TranslationResponseBriefDto:
	- string TranslatedText - Required, Not null, Len<0-300>
5. TranslationDto:
	- int Id
	- string Translation - Required, Not null, Len<1-50>
## Views
1. Translate - a View with input to be translated and a button to send a POST request and use AJAX to modify the output element
## ViewModels
1. TranslationRequestViewModel
	- int? Id
	- string TextToTranslate - Required, Not null, Len[ 1-100 ]
	- DateTime? RequestDate - (if not provided, controller will provide datatime)
	- int? TranslationId - default: 1 (1 - leetspeak)(fot the sake of future updates)
## Controller
TranslationController
1. private GetTranslation(string textToTranslate, string translation) returns: TranslationResponseBrief
2. (/api/Translate/) TranslateApi( FromQuery TranslationRequestCreateDto) returns: IActionResult (JsonResult(TranslationResponseBrief))
3. (/Translate/) Translate( FromBody TranslationRequestViewModel) return: IActionResult (TranslationResponseBrief) (for AJAX)
4. Index () returns IActionResult (View)
## TranslationClient
TranslationClient : ITranslationClient  
ITranslationClient:
1. Translate(string textToTranslate, string translation) returns: Task < FuntranslationsResponse >

## Funtranslations folder
1. record FuntranslationsResponse:
	- (JsonProperty) Success Success
	- (JsonProperty) Contents Contents
2. record Success:
	- (JsonProperty) int Total
3. record Contents:
	- (JsonProperty) string Translated
	- (JsonProperty) string Text
	- (JsonProperty) string Translation

## Final remarks
This document was needed to actually start programming. Planning helps in addressing many problems which could turn up during development. Handling them before any implementation has been done is much less resource-consuming.  
A solution meant to be maintainable and extendable in the long run certainly benefits from careful planning. Of course, the final product may vary in places from the plan as it is hard to predict every possible hurdle.