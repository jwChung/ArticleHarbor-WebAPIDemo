namespace ArticleHarbor.DomainModel
{
    using System;

    [Flags]
    public enum Roles
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The user
        /// </summary>
        User = 1,

        /// <summary>
        /// The author
        /// </summary>
        Author = 2,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = 4
    }

    public enum Role
    {
        /// <summary>
        /// The user
        /// </summary>
        User = Permissions.Authentication,

        /// <summary>
        /// The author
        /// </summary>
        Author = User | Permissions.WriteArticle | Permissions.DeleteOwnArticle,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = Author | Permissions.DeleteAnyArticle
    }

    [Flags]
    public enum Permissions
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