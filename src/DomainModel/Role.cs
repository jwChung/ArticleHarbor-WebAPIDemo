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
        User = Permissions.Authentication,

        /// <summary>
        /// The author
        /// </summary>
        Author = User | Permissions.CreateArticle | Permissions.DeleteOwnArticle | Permissions.ModifyOwnArticle,

        /// <summary>
        /// The administrator
        /// </summary>
        Administrator = Author | Permissions.DeleteAnyArticle | Permissions.ModifyAnyArticle
    }
}