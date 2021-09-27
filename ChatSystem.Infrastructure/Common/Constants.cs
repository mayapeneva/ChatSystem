namespace ChatSystem.Infrastructure.Common
{
    public static class Constants
    {
        public const string DefaultValidationInstance = "Attempted validation for an instance with a defaul value (null) => {0}. Prevent null instance validation attempts by adding additional validation rules in the parent object.";

        public const string Exchange = "chat-messages";

        public const string Key = "messages-key";

        public const string Queue = "messages-queue";
    }
}
