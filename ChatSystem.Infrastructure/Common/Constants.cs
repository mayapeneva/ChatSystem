namespace ChatSystem.Infrastructure.Common
{
    public static class Constants
    {
        public const string CouldNotLoadMessages = "Could not load messages.";

        public const string CouldNotSaveMessages = "Could not save messages.";

        public const string DefaultValidationInstance = "Attempted validation for an instance with a defaul value (null) => {0}. Prevent null instance validation attempts by adding additional validation rules in the parent object.";

        public const string Exchange = "chat-messages";

        public const string Key = "messages-key";

        public const string NoMessagesFound = "No messages found.";

        public const string Queue = "messages-queue";
    }
}
