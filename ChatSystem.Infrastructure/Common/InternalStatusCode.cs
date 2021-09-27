namespace ChatSystem.Infrastructure.Common
{
    using System.Net;

    public static class InternalStatusCode
    {
        public const int Success = (int)HttpStatusCode.OK;

        public const int NotFound = (int)HttpStatusCode.NotFound;

        public const int BadRequest = (int)HttpStatusCode.BadRequest;

        public const int Conflict = (int)HttpStatusCode.Conflict;

        public const int InternalServerError = (int)HttpStatusCode.InternalServerError;
    }
}
