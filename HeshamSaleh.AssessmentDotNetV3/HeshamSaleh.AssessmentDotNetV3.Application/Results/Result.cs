using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace HeshamSaleh.AssessmentDotNetV3.Application.Results
{
    public class Result : Notifiable<Notification>
    {
        public bool Success { get { return !Notifications.Any(); } }

        protected Result() { }

        protected Result(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result(notifications);
        }

        public static Result Error(IDictionary<string, string[]> errors)
        {
            var notifications = errors.Select((keyvalue) =>
            {
                return new Notification(keyvalue.Key, string.Join(",", keyvalue.Value));
            }).ToList().AsReadOnly();
            return new Result(notifications);
        }

    }

    public class Result<T> : Notifiable<Notification> where T : class
    {
        public bool Success { get { return !Notifications.Any(); } }

        public T Data { get; }

        private Result(T obj)
        {
            Data = obj;
        }

        private Result(IReadOnlyCollection<Notification> notifications)
        {
            Data = null;
            AddNotifications(notifications);
        }

        public static Result<T> Ok(T obj)
        {
            return new Result<T>(obj);
        }

        public static Result<T> Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result<T>(notifications);
        }

        public static Result<T> Error(IDictionary<string, string[]> errors)
        {
            var notifications = errors.Select((keyvalue) =>
            {
                return new Notification(keyvalue.Key, string.Join(",", keyvalue.Value));
            }).ToList().AsReadOnly();
            return new Result<T>(notifications);
        }
    }
}
