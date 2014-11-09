namespace ArticleHarbor.DomainModel
{
    using System;

    [Flags]
    public enum UserPermissions
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The authentication permission
        /// </summary>
        Authentication = 1,

        /// <summary>
        /// The write article permission
        /// </summary>
        WriteArticle = 2,

        /// <summary>
        /// The delete own article permission
        /// </summary>
        DeleteOwnArticle = 4,

        /// <summary>
        /// The delete any article permission
        /// </summary>
        DeleteAnyArticle = 8,
    }
}