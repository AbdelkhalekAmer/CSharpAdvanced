using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAdvanced.TaskExamples
{
    public class Program
    {
        static object obj = new object();

        static bool flag = false;
        static bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                lock (obj)
                {
                    flag = value;
                    Console.WriteLine($"{Thread.CurrentThread.Name} modified Flag to: {flag}");
                }
            }
        }


        public static void Main()
        {
            // Run_WhenAll();

            //Run_TaskContinuation().Wait();

            Run_AggregateExceptions();

            //Run_Wait_When();

        }

        static void Run_Wait_When()
        {
            try
            {
                var t1 = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    return Thread.CurrentThread.Name = "T1";
                    //var ex = new NotImplementedException("T1");
                    //ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                    //throw ex;

                });
                var t2 = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    Thread.CurrentThread.Name = "T2";
                    var ex = new NotImplementedException("T2");
                    ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                    throw ex;
                });
                var t3 = Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    return Thread.CurrentThread.Name = "T3";
                    //var ex = new NotImplementedException("T3");
                    //ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                    //throw ex;
                });

                // Task.WaitAll(t1, t2, t3); // will catch and throw the exception

                //var x = Task.WaitAny(t1, t2, t3); // Fire and forget
                //Console.WriteLine(x);

                //var x = Task.WaitAny(new[] { t1, t2, t3 }, 1000);
                //Console.WriteLine(x);

                //var t = Task.WhenAll(t1, t2, t3); // will catch and throw the exception
                //t.Wait();

                //var t = Task.WhenAny(t1, t2, t3); // Fire and forget
                //t.Wait();


            }
            catch (AggregateException ex)
            {

                ex.Flatten().Handle(exc =>
                {
                    Console.WriteLine(string.IsNullOrWhiteSpace(Convert.ToString(Task.CurrentId)) ? Thread.CurrentThread.Name : Convert.ToString(Task.CurrentId));
                    var thrower = exc.Data["Thrower"];
                    if (Convert.ToString(thrower) == "T1") return false;

                    Console.WriteLine("==========================================");
                    Console.WriteLine($"{exc.Data["Thrower"]}: {exc.Message}");
                    Console.WriteLine("==========================================");

                    return true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("********************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("********************************************");
            }
        }

        static void Run_AggregateExceptions()
        {
            try
            {
                try
                {
                    var t1 = Task.Run(() =>
                    {
                        Thread.CurrentThread.Name = "T1 parent";
                        Task.Run(() =>
                        {
                            try
                            {
                                Task.Run(() =>
                                {
                                    Thread.CurrentThread.Name = "T1 child child";
                                    var ex = new NotImplementedException("T1 child child");
                                    ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                                    throw ex;
                                }).Wait();

                            }
                            catch (AggregateException ex)
                            {
                                throw ex.Flatten();
                            }
                        }).Wait();

                    });

                    #region Use Task.Factory.StartNew and use creation option attached to parent
                    //var t1 = Task.Factory.StartNew(() =>
                    //{
                    //    Thread.CurrentThread.Name = "T1 parent";
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        try
                    //        {
                    //            var cc = Task.Factory.StartNew(() =>
                    //            {
                    //                Thread.CurrentThread.Name = "T1 child child";
                    //                var ex = new NotImplementedException("T1 child child");
                    //                ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                    //                throw ex;
                    //            }, TaskCreationOptions.AttachedToParent);

                    //        }
                    //        catch (AggregateException ex)
                    //        {
                    //            throw ex.Flatten();
                    //        }
                    //    }, TaskCreationOptions.AttachedToParent);

                    //});
                    #endregion

                    var t2 = Task.Run(() =>
                    {
                        Thread.CurrentThread.Name = "T2";
                        var ex = new NotImplementedException("T2");
                        ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                        throw ex;
                    });
                    var t3 = Task.Run(() =>
                    {
                        Thread.CurrentThread.Name = "T3";
                        var ex = new NotImplementedException("T3");
                        ex.Data.Add("Thrower", Thread.CurrentThread.Name);
                        throw ex;
                    });

                    Task.WaitAll(t1, t2, t3); // the same as 
                    // Task.WhenAll(t1, t2, t3).Wait();

                }
                catch (AggregateException ex)
                {

                    ex.Flatten().Handle(exc =>
                    {
                        Console.WriteLine(string.IsNullOrWhiteSpace(Convert.ToString(Task.CurrentId)) ? Thread.CurrentThread.Name : Convert.ToString(Task.CurrentId));
                        var thrower = exc.Data["Thrower"];
                        if (Convert.ToString(thrower) == "T1") return false;

                        Console.WriteLine("==========================================");
                        Console.WriteLine($"{exc.Data["Thrower"]}: {exc.Message}");
                        Console.WriteLine("==========================================");

                        return true;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("********************************************");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("********************************************");
                }
            }
            catch (AggregateException ex)
            {
                ex.Handle(exc =>
                {
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-");
                    Console.WriteLine($"{exc.Data["Thrower"]}: {exc.Message}");
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-");
                    return true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task Run_TaskContinuation()
        {
            try
            {
                var t1 = Task.Run(() =>
                {
                    Thread.CurrentThread.Name = "T1";
                    Flag = true;
                    return "T1";
                });
                var t2 = Task.Run(() =>
                {
                    //Thread.Sleep(1000);
                    Thread.CurrentThread.Name = "T2";
                    if (Flag)
                    {
                        throw new NotImplementedException();
                    }
                    return "T2";
                });
                var t3 = Task.Run(() =>
                {
                    //Thread.Sleep(3000);
                    Thread.CurrentThread.Name = "T3";
                    Flag = false;
                    return "T3";
                });

                var t4 = await Task.WhenAll(t1, t2, t3).ContinueWith(data =>
                {
                    foreach (var item in data.Result)
                    {
                        Console.WriteLine(item);
                    }
                    return data;
                });

                //Task.Factory.ContinueWhenAll(new[] { t1, t2, t3 }, x => { Task.WaitAll(x); }).Wait();

                Console.WriteLine("Flag result: {0}", Flag);

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var exc in ex.InnerExceptions)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine(exc);
                    Console.WriteLine("==========================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("********************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("********************************************");
            }
        }

        static void Run_WhenAll()
        {
            Task t = null;

            try
            {
                var t1 = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("T1");
                });
                var t2 = new Task(() => throw new NotImplementedException());
                var t3 = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("T3");
                });

                t1.Start();
                t2.Start();
                t3.Start();

                //t1.Wait();
                //t2.Wait();
                //t3.Wait();

                //Task.WaitAll(t2, t1, t3);

                t = Task.WhenAny(t1, t2, t3)
                    .ContinueWith((x) =>
                    {
                        try
                        {
                            x.Wait();
                            Console.WriteLine("T4");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    });

                t.Wait();


            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var exc in ex.InnerExceptions)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine(exc);
                    Console.WriteLine("==========================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("********************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("********************************************");
            }

            if (t.Status == TaskStatus.RanToCompletion)
                Console.WriteLine("Succeeded.");
            else if (t.Status == TaskStatus.Faulted)
                Console.WriteLine("Failed");
        }

    }
}
