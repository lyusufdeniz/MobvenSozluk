namespace MobvenSozluk.Domain.Constants
{
    public static class MagicStrings
    {
        #region FilterCases
        public const string FilterCaseMinimum = "min";
        public const string FilterCaseMaximum = "max";
        public const string FilterCaseEqual = "equal";
        #endregion
        #region CacheKeys
        public const string CategoriesCacheKey = "Categories";
        public static string CategoryCacheKey(int categoryId)
        {
            return $"{"category_"}{categoryId}";
        }
        public const string RoleCacheKey = "Roles";
        #endregion

        #region ExceptionValues

        public const string BadRequestExceptionValue = "BadRequestException";
        public const string NotFoundExceptionValue = "NotFoundException";
        public const string ArgumentNullExceptionValue = "ArgumentNullException";
        public const string NotImplementedExceptionValue = "NotImplementedException";
        public const string KeyNotFoundExceptionValue = "KeyNotFoundException";
        public const string ConflictExceptionValue = "ConflictException";
        public const string ForbiddenExceptionValue = "ForbiddenException";
        public const string UnauthorizedAccessExceptionValue = "UnauthorizedAccessException";
        #endregion
        #region ErrorMessages
        public static string NotFoundMessage<T>()
        {
            return $"{typeof(T).Name} not found";
        }
        public const string UserNameOrPasswordWrong = "User name or password wrong";
        public const string UserOrTokenNotFound = "User not found or token expired";
        public const string BadRequestDescription = "An error occurred while requesting the API";
        public const string UserAlreadyExist = "User already exist";
        public const string RoleAlreadyExist = "Role already exist";
        public const string RoleNotExist = "There is no such role name exist";
        public const string AlreadyLogin = "You cannot log in cuz you are already logged in";
        public const string AlreadyLogout = "You cannot log out cuzz you are already logged out";
        public const string RefreshTokenExpire = "Refresh token has expired";
        public const string SearchKeywordCantBeNull = "Search keyword can't be null";
        #endregion




    }
}
