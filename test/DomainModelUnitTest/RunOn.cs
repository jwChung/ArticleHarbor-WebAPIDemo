namespace ArticleHarbor
{
    public enum RunOn
    {
        /// <summary>
        /// Run tests only on CI.
        /// </summary>
        CI,

        /// <summary>
        /// Run tests on wherever.
        /// </summary>
        Any,

        /// <summary>
        /// Run tests only on local.
        /// </summary>
        Local
    }
}