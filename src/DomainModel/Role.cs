namespace ArticleHarbor.DomainModel
{
    public enum Role
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The user
        /// </summary>
        User = UserPermissions.Authentication,

        /// <summary>
        /// The author
        /// </summary>
        Author = User | UserPermissions.WriteArticle | UserPermissions.DeleteOwnArticle,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = Author | UserPermissions.DeleteAnyArticle
    }
}