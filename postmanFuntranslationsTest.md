# Postman Funtranslations API Tests
I used Postman to test all of these calls.
## Why these tests
The reason for testing Funtranslations' API is that we got no business rules which cover this area. Seconly, [its documentation](https://funtranslations.com/api/#leetspeak) doesn't really offer enough information. It allowed us to make a code representation of the JSON returned and gave us information on how to make API calls.

The problem is we got no information about limitations of this API. We know that the [ratelimit](https://funtranslations.com/api/#ratelimit) is set to 60 calls a day distrubuted to 5 calls an hour. But we do not know what the error will look like.  
We also don't know if there is a character limit, if some characters (like whitespace ones) are not allowed, etc.
## What do we want from our tests
- We want to get a relatively good picture of what we can expect.
- We want to be able to set some conservative boundries. (even if Funtranslations API would allow something doesn't mean we should allow it to the same extent)
- We want to know the most common errors we can encounter.
- We don't want to have a full coverage of everything that can happen, we want to lay foundation for future developers to build on.
- We want to be able to unit test our API calling service.
# Tests
## Test 1 - determinism
We send the following request using Postman:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=Hello World Hello World Hello World Hello World Hello World</pre>

We get the following response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "H3Ll0 w0Rl|) h3ll0 \/\/0rld h311() \/\/orld ]-[E1lO W()rl|) ]-[3L1o \/\/0R1D",
        "text": "Hello World Hello World Hello World Hello World Hello World",
        "translation": "leetspeak"
    }
}
```
So far so good but let's call it once more:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "]-[3110 W()r1|) ]-[3l10 W()rld HEll0 \/\/OR1|) hEllO w()Rl|) ]-[3110 wORl|)",
        "text": "Hello World Hello World Hello World Hello World Hello World",
        "translation": "leetspeak"
    }
}
```
Now we can see the problem, from user's perspective the API is non-deterministic.  
What is also worth noting is that certain characters like 'H' and 'W' can be changed into ```"]-["``` and ```"\/\/"``` . Therefore, the translated can have quite a different length.
What it means we cannot and shouldn't have integration tests checking content of translated and length of translated.  
Moreover, we can see that our database can't be too restrictive when it comes to translated text column and it should also be longer than the text to translate.

### Takeaways
- Integration tests won't test content of translated
- Some characters can be replaced for text 3 or 4 times longer
- Database column for translated should have a higher length limit than text to translate
- Instead of having 4x max length of text to translate we may have it at 2x and trim it if needed before inserting it to database (can be changed later if we 100% need every word recorded)

## Test 2 - extremes
We send the following request using Postman:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=</pre>

We get the following response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "",
        "text": "",
        "translation": "leetspeak"
    }
}
```
As we can see the API accepts empty strings and translated is also empty.  
We have to consider that sending such a request still requires the use of http protocol and as such is both a waste of time and resources.

Let's test the following request using Postman:
<pre>https://api.funtranslations.com/translate/leetspeak.json</pre>

We get the following response:
```
{
    "error": {
        "code": 400,
        "message": "Bad Request: text is missing."
    }
}
```
Thanks to that we get to see some sort of error which is good as we now know the general structure of this API's errors.

Let's try a one letter request:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=A</pre>

The response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "a",
        "text": "A",
        "translation": "leetspeak"
    }
}
```
Let's now try a lot of text, a bit over 500 characters without whitespace characters other than space:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you</pre>

Response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "Hi, h3Y, h1y4, hell0, wELc0me, 1337 T0 C j00, ho\/\/ j00 doIgn, It'Z b3En 5o lo]\\[G, |'m ZO hAppY 70 C j00, ]-[I, h3Y, HIY4, h3lLo, W3lk0me, 1337 T0 C j00, hOw j00 d01n, i7's b3eN S0 l0N6, !'M Zo hAppY t() C j00, ]-[!, ]-[ey, hIYa, hE1l0, \/\/e1k0m3, 1337 7O C j00, ]-[0w j00 |)01n, iT'5 8eE|\\| z() lOng, !'m so h4Ppy T0 C j00, hY, h3y, HYy4, h3LlO, weLk0mE, 1337 to C j00, h()W j00 do!N, it'Z b3eN 5o 1()|\\|G, Y'm S0 ]-[appy To C j00, ]-[!, ]-[3y, HYY4, h3Llo, w3lcOmE, 1337 TO C j00, hOW j00 d01n, I7'Z bEen 50 long, !']\/[ ZO haPPy TO C j00",
        "text": "Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you, Hi, Hey, Hiya, Hello, Welcome, Good to see you, How you doin, It's been so long, I'm so happy to see you",
        "translation": "leetspeak"
    }
}
```
As we can see 500 characters is not a problem for this API.
I don't have any user stories as such I can't really deduce how much do we need, so I will set 500 as a limit for now.

### Takeaways
- Technically, the API won't have a problem with 0-character long strings but let us make the lower limit of at least 1 character
- We can safely set the upper limit of characters to 500

## Test 3 - incorrect translation
Let's use the following request with the name of translation misspelled:
<pre>https://api.funtranslations.com/translate/yeetspeak.json?text=Hello World Hello World Hello World Hello World Hello World</pre>

We get the following response:
```
{
    "error": {
        "code": 404,
        "message": "Not Found"
    }
}
```
## Test 4 - whitespace characters and special characters
Let's call a request with newline whitespace characters:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=I shall now test new line
I wonder if it works not testing tab for a good measure
:D - smile
>:( - angry
:( - sad</pre>

The response:
```
{
    "error": {
        "code": 404,
        "message": "Not Found"
    }
}
```
As we can see new line couses an error.

Let's check a tabulator:
<pre>https://api.funtranslations.com/translate/leetspeak.json?text=I shall not test new line I wonder if it works	testing tab for a good measure :D - smile >:( - angry :( - sad</pre>

The response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - 4ngRy :( - 5Ad",
        "text": "I shall not test new line I wonder if it works\ttesting tab for a good measure :D - smile >:( - angry :( - sad",
        "translation": "leetspeak"
    }
}
```
This one appears to work.

Let's test special characters:
```
https://api.funtranslations.com/translate/leetspeak.json?text=!@%23$%^%26*()-_=+,./<>?'"\|[]{}`~
```
The response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "!@#$%^&*()-_= ,./<>?'\"\\|[]{}`~",
        "text": "!@#$%^&*()-_= ,./<>?'\"\\|[]{}`~",
        "translation": "leetspeak"
    }
}
```
It seems that special characters don't need a special treatment.

Let's test some more unusual characters:
```
https://api.funtranslations.com/translate/leetspeak.json?text=ÂÆĆ↚↹⅛⁷₉Ⅳ⨆⨭αΧξ৲₼₲ʣ
```
The response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "ÂÆĆ↚↹⅛⁷₉Ⅳ⨆⨭αΧξ৲₼₲ʣ",
        "text": "ÂÆĆ↚↹⅛⁷₉Ⅳ⨆⨭αΧξ৲₼₲ʣ",
        "translation": "leetspeak"
    }
}
```
No problems here.

Let's now test emojis:
```
https://api.funtranslations.com/translate/leetspeak.json?text=😀😎😪😴😨🤯🤠👽😼🙉🐔
```
The response:
```
{
    "success": {
        "total": 1
    },
    "contents": {
        "translated": "😀😎😪😴😨🤯🤠👽😼🙉🐔",
        "text": "😀😎😪😴😨🤯🤠👽😼🙉🐔",
        "translation": "leetspeak"
    }
}
```
No problems here as well.
### Takeaways
- Don't allow a newline whitespace character.
- Special characters can be used without worry.