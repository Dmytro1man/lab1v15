using System;

namespace Lab3
{
    // Варіант 15: Клас VideoRecorder із підтримкою патерну Dispose
    public class VideoRecorder : IDisposable
    {
        // Прапорець для відстеження, чи було вже викликано Dispose
        private bool _disposed = false;

        // Поля класу
        private string _outputFile;
        private bool _isRecording;

        // Публічні властивості
        public string OutputFile
        {
            get => _outputFile;
            set => _outputFile = value;
        }

        public bool IsRecording => _isRecording;

        // Конструктор: "виділяє ресурс" та запускає запис
        public VideoRecorder(string outputFile)
        {
            _outputFile = string.IsNullOrWhiteSpace(outputFile) ? "default_video.mp4" : outputFile;
            Console.WriteLine($"[Constructor] Відеозаписувач створено для файлу '{_outputFile}'.");
            StartRecording();
        }

        // Методи керування записом
        public void StartRecording()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(VideoRecorder), "Неможливо почати запис: об'єкт уже знищено.");
            }

            _isRecording = true;
            Console.WriteLine($"[VideoRecorder] Розпочато відеозапис у файл '{_outputFile}'.");
        }

        public void StopRecording()
        {
            if (_isRecording)
            {
                _isRecording = false;
                Console.WriteLine($"[VideoRecorder] Запис зупинено для файлу '{_outputFile}'.");
            }
        }

        // Захищений віртуальний метод Dispose(bool disposing) — ядро патерну Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Звільнення керованих ресурсів (викликається лише при Dispose())
                    Console.WriteLine($"[Dispose(true)] Звільнення керованих ресурсів для '{_outputFile}'...");
                }

                // Звільнення некерованих ресурсів / системних дій (викликається і з Dispose, і з деструктора)
                if (_isRecording)
                {
                    Console.WriteLine($"[Dispose] Аварійна/планова зупинка запису файлу '{_outputFile}'...");
                    StopRecording();
                }

                Console.WriteLine($"[Dispose] Файлові дескриптори та ресурси для '{_outputFile}' успішно звільнено.");
                _disposed = true;
            }
        }

        // Публічний метод Dispose (реалізація IDisposable)
        public void Dispose()
        {
            Dispose(true);
            // Повідомляємо GC, що фіналізатор викликати більше не потрібно
            GC.SuppressFinalize(this);
        }

        // Деструктор (Фіналізатор)
        ~VideoRecorder()
        {
            Console.WriteLine($"\n[Finalizer] Викликано деструктор ~VideoRecorder() для '{_outputFile}'!");
            Dispose(false);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== СЦЕНАРІЙ 1: Використання конструкції 'using' ===");
            using (VideoRecorder recorder1 = new VideoRecorder("lecture_cam1.mp4"))
            {
                Console.WriteLine("  --> [Main] Іде процес запису лекції у блоці using...");
            } // ТУТ автоматично й детерміновано викликається Dispose()
            Console.WriteLine("=== Сценарій 1 завершено ===\n");


            Console.WriteLine("=== СЦЕНАРІЙ 2: Явний виклик Dispose() без using ===");
            VideoRecorder recorder2 = new VideoRecorder("stream_game.mp4");
            Console.WriteLine("  --> [Main] Іде процес запису стріму...");
            recorder2.StopRecording();
            recorder2.Dispose(); // Явний виклик звільнення ресурсів
            Console.WriteLine("=== Сценарій 2 завершено ===\n");


            Console.WriteLine("=== СЦЕНАРІЙ 3: Об'єкт без виклику Dispose() (Робота GC та деструктора) ===");
            CreateAndForgetRecorder();

            Console.WriteLine("  --> [Main] Запускаємо примусове збирання сміття (GC.Collect)...");
            GC.Collect();
            GC.WaitForPendingFinalizers(); // Чекаємо виконання фіналізатора у фоновому потоці
            Console.WriteLine("=== Сценарій 3 завершено ===\n");

            Console.WriteLine("=== Усі демонстраційні сценарії виконано успішно ===");
        }

        // Окремий метод для Сценарію 3, щоб локальна змінна втратила область видимості
        static void CreateAndForgetRecorder()
        {
            VideoRecorder recorder3 = new VideoRecorder("unmanaged_security.mp4");
            Console.WriteLine("  --> [CreateAndForgetRecorder] Об'єкт створено, але Dispose() не викликається.");
            // Метод завершується, посилання на recorder3 втрачається, об'єкт стає кандидатом для GC
        }
    }
}