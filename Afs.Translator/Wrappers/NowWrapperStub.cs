namespace Afs.Translator.Wrappers
{
    public class NowWrapperStub : INowWrapper
    {
        public NowWrapperStub(DateTime now) 
        {
            date = now;
        }
        public DateTime date { get; set; }
        public DateTime Now => date;
    }
}
