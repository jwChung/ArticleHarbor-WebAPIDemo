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
        Author = User | UserPermissions.CreateArticle | UserPermissions.DeleteOwnArticle | UserPermissions.ModifyOwnArticle,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = Author | UserPermissions.DeleteAnyArticle | UserPermissions.ModifyAnyArticle
    }
}