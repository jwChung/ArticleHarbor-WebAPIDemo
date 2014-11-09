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
        /// The create article permission
        /// </summary>
        CreateArticle = 2,

        /// <summary>
        /// The delete own article permission
        /// </summary>
        DeleteOwnArticle = 4,

        /// <summary>
        /// The delete any article permission
        /// </summary>
        DeleteAnyArticle = 8,

        /// <summary>
        /// The modify own article permission
        /// </summary>
        ModifyOwnArticle = 0x10,

        /// <summary>
        /// The modify any article permission
        /// </summary>
        ModifyAnyArticle = 0x20
    }
}