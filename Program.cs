


//Запуск задачи и ожидание её завершения Напишите программу, которая создаёт одну задачу, выполняет в ней вычисление (например, подсчёт суммы чисел от 1 до 100),
//а затем выводит результат. Используйте метод Task.Wait() для ожидания завершения задачи.



Task<int> sumTask = new Task<int>(() =>
{
    var summa = 0;
    for (var i = 1; i <= 100; i++)
    {
        summa += i;
    }
    return summa;
});


sumTask.Start();
//sumTask.Wait();
int result = sumTask.Result;
Console.WriteLine($"суммa чисел от 1 до 100 = {result}");






//----------------------------------------------------------------
//Последовательный запуск нескольких задач Создайте программу, в которой одна задача выполняет вычисление и передает результат следующей задаче. 
//Первая задача должна подсчитать сумму всех чётных чисел от 1 до 100, а вторая — умножить результат на 2 и вывести его.


//Task<int> multTask = Task.Factory.StartNew(() =>
//{
//    Task<int> sumTask = Task.Factory.StartNew<int>(() =>
//    {
//        var sum = 0;
//        for (var i = 1; i <= 100; i++)
//        {
//            if (i % 2 == 0)
//            {
//                sum += i;
//            }
//        }

//        Console.WriteLine($"суммa всех чётных от 1 до 100 = {sum}");
//        return sum;
//    }, TaskCreationOptions.AttachedToParent);
//    int mult = sumTask.Result * 2;
//    return mult;
//});

//int result = multTask.Result;
//Console.WriteLine($"Удвоенная суммa всех чётных от 1 до 100 = {result}");

//----------------------------------------------------------------
//Task<int> sumTask = Task.Factory.StartNew(() =>
//{
//    int sum = 0;
//    for (int i = 1; i <= 100; i++)
//    {
//        if (i % 2 == 0)
//        {
//            sum += i;
//        }
//    }
//    Console.WriteLine($"Сумма всех четных чисел от 1 до 100 = {sum}");
//    return sum;
//});


//Task<int> multTask = sumTask.ContinueWith(task =>
//{
//    int result = task.Result * 2;
//    Console.WriteLine($"Удвоенная сумма всех четных чисел от 1 до 100 = {result}");
//    return result;
//});

//multTask.Wait();




//----------------------------------------------------------------
//Работа с массивом задач Напишите программу, которая запускает 5 задач, каждая из которых выполняет имитацию длительной работы с помощью Thread.Sleep() 
//на разное количество миллисекунд (например, 1000, 1500, 2000, 2500, 3000). Используйте Task.WhenAll(), чтобы дождаться завершения всех задач, и выведите сообщение о завершении каждой задачи.

//Task[] tasks = new Task[5];
//for (int i = 0; i < tasks.Length; i++)
//{
//    int TaskIndex = i;
//    int sleepTime = 1000 + TaskIndex * 500;
//    tasks[i] = new Task(() =>
//    {
//        Thread.Sleep(sleepTime);
//        Console.WriteLine($"Task{TaskIndex} завершен, время ожидания {sleepTime} ");
//    });

//    tasks[i].Start();
//}


//Task.WaitAll(tasks);
//Console.WriteLine("Все задачи завершены");




//----------------------------------------------------------------
//Результат задачи с использованием Task<TResult> Создайте программу, в которой одна задача возвращает результат вычисления (например, возведение числа в степень), 
//а вторая задача использует результат первой задачи для выполнения дальнейших действий (например, вывод числа в консоль). Используйте Task<TResult> для получения результата.

//double x = 5;
//double y = 2;
//Task<double> powTask = new Task<double>(() => Math.Pow(x, y));

//Task printTask = powTask.ContinueWith(task => PrintResult(task.Result));

//powTask.Start();

//printTask.Wait();
//Console.WriteLine("Конец метода");

//int Sum(int a, int b) => a + b;
//void PrintResult(double sum) => Console.WriteLine($" числo {x} в степени {y}: {sum}");



//----------------------------------------------------------------
//Цепочка задач с обработкой исключений Напишите программу, в которой несколько задач выполняются последовательно, каждая из них берёт результат предыдущей. 
//Например, первая задача вычисляет сумму чисел, вторая — делит её на случайное число, третья — возводит результат в степень. В случае, если возникает исключение (например, деление на ноль), программа должна корректно обработать его с помощью Task.ContinueWith().


//using System;
//Random random = new Random();
//int num = random.Next(0, 101);
//int y = 2;

//Task<int> task1 = new Task<int>(() => Sum(4, 5));

//Task<double> task2 = task1.ContinueWith(task =>
//{
//    try
//    {
//        Console.WriteLine($"Деление {task.Result} на {num}");
//        return Del(task.Result, num);
//    }
//    catch (DivideByZeroException)
//    {
//        Console.WriteLine("Возникло исключение DivideByZeroException в задаче 2");
//        return 0;
//    }
//});

//Task<double> task3 = task2.ContinueWith(task => Math.Pow(task.Result, y));


//Task task4 = task3.ContinueWith(t =>
//    Console.WriteLine($"Результат: {t.Result}"));

//task1.Start();

//task4.Wait();
//Console.WriteLine("Конец метода ");




//int Sum(int a, int b) => a + b;

//double Del(double a, int b)
//{
//    if (b == 0)
//        throw new DivideByZeroException();
//    return a / b;
//}







//----------------------------------------------------------------
//Ожидание первой завершившейся задачи Создайте программу, которая запускает 3 задачи с разным временем задержки (например, 2000, 3000 и 5000 мс) и использует 
//Task.WhenAny() для ожидания первой завершившейся задачи. После этого программа должна вывести результат первой завершившейся задачи и завершить работу.

//Task[] tasks = new Task[3];
//int[] sleepTimes = { 2000, 3000, 5000 };
//for (int i = 0; i < tasks.Length; i++)
//{
//    int TaskIndex = i;
//    int sleepTime = sleepTimes[TaskIndex];

//    tasks[i] = new Task(() =>
//    {
//        Thread.Sleep(sleepTime);
//        Console.WriteLine($"Task {TaskIndex} завершен, время ожидания {sleepTime} мс");
//    });

//    tasks[i].Start();
//}


//Task.WaitAny(tasks);
//Console.WriteLine("Все задачи завершены");




//Task[] tasks = new Task[3];
//int[] sleepTimes = { 2000, 3000, 5000 };
//for (int i = 0; i < tasks.Length; i++)
//{
//    int TaskIndex = i;
//    int sleepTime = sleepTimes[TaskIndex];

//    tasks[i] = new Task(() =>
//    {
//        Thread.Sleep(sleepTime);
//        Console.WriteLine($"Task {tasks[TaskIndex].Id} завершен, время ожидания {sleepTime} мс");
//    });

//    tasks[i].Start();
//}


//Task first = Task.WhenAny(tasks).Result;
//Console.WriteLine($"Первая завершившаяся задача {first.Id}");
//Console.WriteLine("Все задачи завершены");




//----------------------------------------------------------------
//Выполнение задачи с тайм-аутом Напишите программу, которая запускает задачу, выполняющую длительное вычисление (например, подсчёт факториала)

//Task<long> factorialTask = Task.Run(() => Factorial(4));
//int sleepTimes = 1000;
//if (factorialTask.Wait(sleepTimes))
//{
//    long result = factorialTask.Result;
//    Console.WriteLine($"Факториал: {result}");
//}
//else
//{
//    Console.WriteLine($"Операция отменена из-за превышения времени выполнения {sleepTimes}s.");
//}

//Console.WriteLine("Конец программы.");
//long Factorial(int n)
//{
//    long result = 1;
//    for (int i = 1; i <= n; i++)
//    {

//        result *= i;
//        Thread.Sleep(100);
//    }
//    return result;
//}