namespace ArticleHarbor.DomainModel
{
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
}