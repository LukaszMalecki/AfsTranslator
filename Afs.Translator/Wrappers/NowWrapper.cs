﻿namespace Afs.Translator.Wrappers
{
    public class NowWrapper : INowWrapper
    {
        public DateTime Now => DateTime.Now;
    }
}
