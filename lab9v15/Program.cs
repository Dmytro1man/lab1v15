using System;
using System.Collections.Generic;
using System.Text;

namespace lab9v15
{
    // Клас для представлення сповіщення
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            return $"[#{Id}] Для {Username}: {Message}";
        }
    }

    public class NotificationSystem
    {
        // HashSet: Унікальні користувачі з увімкненими сповіщеннями
        private HashSet<string> _subscribedUsers = new HashSet<string>();
        
        // Queue: Черга сповіщень для відправки (FIFO)
        private Queue<Notification> _notificationQueue = new Queue<Notification>();
        
        // Stack: Історія прочитаних/відправлених сповіщень (LIFO)
        private Stack<Notification> _notificationHistory = new Stack<Notification>();
        
        private int _notificationCounter = 1;

        // --- Операції з HashSet ---
        public void SubscribeUser(string username)
        {
            if (_subscribedUsers.Add(username))
            {
                Console.WriteLine($"[HashSet] Користувача '{username}' успішно підписано на сповіщення.");
            }
            else
            {
                Console.WriteLine($"[HashSet] Користувач '{username}' вже підписаний (дублікат проігноровано).");
            }
        }

        public void UnsubscribeUser(string username)
        {
            if (_subscribedUsers.Remove(username))
            {
                Console.WriteLine($"[HashSet] Користувач '{username}' відписався від сповіщень.");
            }
        }

        public bool IsUserSubscribed(string username)
        {
            return _subscribedUsers.Contains(username);
        }

        // --- Операції з Queue ---
        public void EnqueueNotification(string username, string message)
        {
            if (!IsUserSubscribed(username))
            {
                Console.WriteLine($"[Система] Помилка: Користувач '{username}' не підписаний на сповіщення.");
                return;
            }

            var notification = new Notification { Id = _notificationCounter++, Message = message, Username = username };
            _notificationQueue.Enqueue(notification);
            Console.WriteLine($"[Queue] Сповіщення додано в чергу. (В черзі: {_notificationQueue.Count})");
        }

        public void ProcessNextNotification()
        {
            if (_notificationQueue.Count == 0)
            {
                Console.WriteLine("[Queue] Черга сповіщень порожня.");
                return;
            }

            // Перегляд першого елемента без видалення
            var nextNotif = _notificationQueue.Peek();
            Console.WriteLine($"[Queue Peek] Наступне сповіщення до обробки: {nextNotif}");

            // Вилучення та обробка (FIFO)
            var notification = _notificationQueue.Dequeue();
            Console.WriteLine($"[Queue Dequeue] Обробляється сповіщення: {notification}");

            // Збереження в історію
            _notificationHistory.Push(notification);
            Console.WriteLine($"[Stack Push] Сповіщення додано до історії прочитаних.");
        }

        // --- Операції зі Stack ---
        public void ShowLastReadNotification()
        {
            if (_notificationHistory.Count == 0)
            {
                Console.WriteLine("[Stack] Історія сповіщень порожня.");
                return;
            }

            // Перегляд вершини стеку без видалення
            var lastNotif = _notificationHistory.Peek();
            Console.WriteLine($"[Stack Peek] Останнє прочитане сповіщення: {lastNotif}");
        }

        public void UndoLastNotification()
        {
            if (_notificationHistory.Count == 0)
            {
                Console.WriteLine("[Stack] Немає сповіщень для скасування.");
                return;
            }

            // Видалення з вершини стеку (LIFO)
            var poppedNotif = _notificationHistory.Pop();
            Console.WriteLine($"[Stack Pop] Сповіщення видалено з історії: {poppedNotif}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №9. Система обробки сповіщень ===\n");

            var system = new NotificationSystem();

            Console.WriteLine("--- 1. Робота з HashSet (Підписка користувачів) ---");
            system.SubscribeUser("Dmitry");
            system.SubscribeUser("Alice");
            system.SubscribeUser("Dmitry"); // Спроба додати дублікат
            system.UnsubscribeUser("Alice");
            Console.WriteLine();

            Console.WriteLine("--- 2. Робота з Queue (Додавання в чергу) ---");
            system.EnqueueNotification("Dmitry", "Вітаємо в системі!");
            system.EnqueueNotification("Dmitry", "У вас нове повідомлення.");
            system.EnqueueNotification("Bob", "Це повідомлення не дійде."); // Bob не підписаний
            Console.WriteLine();

            Console.WriteLine("--- 3. Робота з Queue та Stack (Обробка FIFO та збереження LIFO) ---");
            system.ProcessNextNotification(); // Обробляємо "Вітаємо в системі!"
            system.ProcessNextNotification(); // Обробляємо "У вас нове повідомлення."
            Console.WriteLine();

            Console.WriteLine("--- 4. Робота зі Stack (Перегляд історії) ---");
            system.ShowLastReadNotification(); // Має показати "У вас нове повідомлення."
            system.UndoLastNotification();     // Видаляє "У вас нове повідомлення."
            system.ShowLastReadNotification(); // Тепер має показати "Вітаємо в системі!"
        }
    }
}