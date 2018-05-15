using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    public class Battle
    { 
        private static char[,] map;
        private static int yourHealth;
        private static int enemyHealth;
        private static char youChar;
        private static int yourPosition;
        private static int enemyPosition;
        private static int shootPosition;
        private static int enemyshootPosition;
        private static char shoot;
        private static char enemyChar;
        private static bool gameProcess;
        private static Timer mapdrowTimer;
        private static Timer enemyActionTimer;
        private static ConsoleKeyInfo keyInfo;
        private static Random rand = new Random();
        private static int act;
        
        private const int N = 15;
        private const int M = 30;// map size
        private const char emptyCell = ' ';

        public Battle()
        {
            gameProcess = true;
            youChar = (char)36;
            enemyChar = (char)37;
            shoot = (char)42;
            yourPosition = M / 2;
            enemyPosition = M / 2;
            map = generateBattleMap();
        }
        
        
        private static char[,] generateBattleMap()
        {
           
            map = new char[N, M];
            for (int i = 0; i < M; i++)
            {
                map[0,i] = '#';
            }

            for (int i = 0; i < N; i++)
            {
                map[i,M-1] = '#';
            }

            for (int i = 0; i < M; i++)
            {
                map[N-1,i] = '#';
            }

            for (int i = 0; i < N; i++)
            {
                map[i,0] = '#';
            }

            map[1, enemyPosition] = enemyChar;
            map[N-2, yourPosition] = youChar;
            return map;
            
        }
        
        private static void render()
        {  
                Console.Clear();
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)

                        Console.Write("{0}", map[i, j]);
                    Console.WriteLine();
                }
                Console.WriteLine("YOUR HEALTH:" + yourHealth);
                Console.WriteLine("ENEMY HEALTH:" + enemyHealth);
                //System.Threading.Thread.Sleep(300);
            
        }
         
        private static bool endGame()
        {
            if (yourHealth == 0)
            {
                Console.Clear();
                enemyActionTimer.Dispose();
                mapdrowTimer.Dispose();
                Console.WriteLine("       YOU LOSE        ");
                PlaySound();
                gameProcess = false;
                return gameProcess;
            }
            else if (enemyHealth == 0)
            {
                Console.Clear();
                enemyActionTimer.Dispose();
                mapdrowTimer.Dispose();
                Console.WriteLine("         YOU WIN         ");
                PlaySound();
                gameProcess = false;
                return gameProcess;
            }
            else return true;
        }
        public static void process(int yHealth,int eHealth)
        {
            Console.Title = "BATTLE";
            Console.ForegroundColor = ConsoleColor.Green;
            Battle btl = new Battle();
            yourHealth = yHealth;
            enemyHealth = eHealth;
            render();
            
            do
            {
                mapdrowTimer = new Timer(TimerMapDrowing, null, 0, 2000);
                enemyActionTimer = new Timer(TimerEnemyAction, null, 0, 6000);
                keyInfo = Console.ReadKey();
                action(keyInfo);
            } while (endGame());
            

        }
        private static void action(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.LeftArrow && yourPosition > 1) 
                {
                System.Threading.Thread.Sleep(200);
                map[N - 2, yourPosition] = emptyCell;
                    yourPosition--;
                    map[N - 2, yourPosition] = youChar;
                }
           else if (keyInfo.Key == ConsoleKey.RightArrow && yourPosition < M-2)
            {
                System.Threading.Thread.Sleep(200);
                map[N - 2, yourPosition] = emptyCell;
                yourPosition++;
                map[N - 2, yourPosition] = youChar;
            }
            else if(keyInfo.Key == ConsoleKey.Spacebar)
            {
                Console.Beep();
                shootPosition = yourPosition;
                for(int i = 1; i < N-2;i++)
                {
                    map[i, shootPosition] = shoot;
                }
                render();
                System.Threading.Thread.Sleep(100);
                if (shootPosition == enemyPosition)
                {
                    Console.Beep(1000, 300);
                    enemyHealth--;
                    //System.Threading.Thread.Sleep(100);
                    
                }
                
                
                    for (int i = 1; i < N - 2; i++)
                    {
                        map[i, shootPosition] = emptyCell;
                    }
                
            }

        }
         private static void EnemyAction()
        {
                act = rand.Next(1, 4);
                if (act == 1 && enemyPosition > 1)
                {
                    System.Threading.Thread.Sleep(200);
                    map[1, enemyPosition] = emptyCell;
                    enemyPosition--;
                    map[1, enemyPosition] = enemyChar;
                }
                else if (act == 2 && enemyPosition < M - 2)
                {
                    System.Threading.Thread.Sleep(200);
                    map[1, enemyPosition] = emptyCell;
                    enemyPosition++;
                    map[1, enemyPosition] = enemyChar;
                }
                else if (act == 3)
                {
                    Console.Beep();
                    enemyshootPosition = enemyPosition;
                    for (int i = 2; i < N-1; i++)
                    {
                        map[i, enemyshootPosition] = shoot;
                    }

                    render();
                    //System.Threading.Thread.Sleep(100);
                    if (enemyshootPosition == yourPosition)
                    {
                        Console.Beep(1000, 300);
                        yourHealth--;
                        System.Threading.Thread.Sleep(200);
                    }
                    for (int i = 2; i < N - 1; i++)
                    {
                        map[i, enemyshootPosition] = emptyCell;
                    }

                    //System.Threading.Thread.Sleep(100);
                }

            
        }
        private static void PlaySound()
        {
            const int soundLenght = 100;
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1320, soundLenght);
            Console.Beep(1188, soundLenght);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 6);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1056, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
        }
        private static void TimerMapDrowing(Object o)
        {
            render();
            GC.Collect();
        }

        private static void TimerEnemyAction(Object o)
        {
            EnemyAction();
        }


        

        
    }



       
    }








