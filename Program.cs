using System;
using System.Threading.Tasks;

namespace AsyncTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Note the Wait() method at the end, and the asyn/await keywords used inside the Run() method
            //Wait() indicates to the app not to exit until all the job is done
            Task.Run(async ()=> await MakeCakeAsync()).Wait();
        }
        static bool isBaked;
        static async Task MakeCakeAsync()
        {
            Task<bool> preheatTask = PreheatOvenAsync();
            await AddCakeIngredients();
            bool isPreheaded = await preheatTask;
            
            Task<bool> bakeCakeTask = BakeCakeAsync(isPreheaded);
            await AddFrostingIngredients();
            var coolFrostingTask = CoolFrostingAsync();
            PassTheTime();
            isBaked = await bakeCakeTask;

            Task<bool> coolCakeTask = CoolCakeAsyn(isBaked);

            bool cakeIsCooled = await coolCakeTask;
            bool frostingIsCooled = await coolFrostingTask;

            await FrostCake(cakeIsCooled, frostingIsCooled);

            WriteLine("Cake is served! Bon Appetit!");
        }

        static void WriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH':'mm':'ss")} => {message}");
        }
        static async Task<bool> PreheatOvenAsync()
        {
            WriteLine("Preheating oven...");
            await Task.Delay(15000);
            WriteLine("Oven is ready!");
            return true;
        }

        static async Task AddCakeIngredients()
        {
            int timeAddingEachIngredient = 2000;
            WriteLine("Added cake mix");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added milk");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added vegetable oil");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added eggs");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Cake ingredients mixed!");
        }

        static async Task<bool> BakeCakeAsync(bool isPreheaded)
        {
            // if(isPreheaded)
            // {
                WriteLine("Baking cake...");
                await Task.Delay(15000);
                WriteLine("Cake is done baking!");
                return true;
            // }
            // else
            // {
            //     WriteLine("The oven needs to be preheated before you bake the cake!");
            //     return false;
            // }
        }
        
         static async Task AddFrostingIngredients()
        {
            int timeAddingEachIngredient = 2000;
            WriteLine("Added cream cheese");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added milk");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added vegetable oil");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Added eggs");
            await Task.Delay(timeAddingEachIngredient);
            WriteLine("Frosting ingredients mixed!");
        }
        
        static async Task<bool> CoolFrostingAsync()
        {
            WriteLine("Cooling frosting...");
            await Task.Delay(15000);
            WriteLine("Frosting is cooled!");
            return true;
        }

        //This method is async void because it is always checking 
        //if the cake is baked, and is launched whithout await it,
        //which is nice since, as the name suggests, they are 
        //passing the time. It stop spending the time when the 
        //cake is done baking.
        static async void PassTheTime()
        {
            await CheckAndContinuePassingTheTime("WATCHING TV...");
            await CheckAndContinuePassingTheTime("ATING LUNCH...");
            await CheckAndContinuePassingTheTime("CLEANING THE KITCHEN...");
        }
        static async Task CheckAndContinuePassingTheTime(string activityMessage)
        {
            //Here, I check every certain time if the cake is done baked to stop spending time
            //TODO: But it wold be better if a notification is received
            int timeElapsed = 1;
            bool firstTime = true;
            int timeToCheckIfCakeIsBaked = 1000;

            while (!isBaked && timeElapsed <= 3)
            {
                if(firstTime)
                    WriteLine(activityMessage);
                else
                    WriteLine($"The cake is not baked yet, I'm still {activityMessage}");

                firstTime = false;
                await Task.Delay(timeToCheckIfCakeIsBaked);
                timeElapsed++;
            }
        }
    
        static async Task<bool> CoolCakeAsyn(bool isBaked)
        {
            WriteLine("Cooling cake...");
            await Task.Delay(15000);
            WriteLine("Cake is cooled!");
            return true;
        }

        static async Task FrostCake(bool cakeIsCooled, bool frostingIsCooled)
        {
            WriteLine("Frosting cake...");
            await Task.Delay(2000);
            WriteLine("Cake is frosted!");
        }
    }
}
