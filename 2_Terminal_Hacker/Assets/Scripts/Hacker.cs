using System;
using System.Collections;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    const string MENU = "0";
    const string EASY = "1";
    const string MEDIUM = "2";
    const string HARD = "3";

    Screen currentScreen = Screen.MainMenu;
    string password;
    int waitingCounter = 0;
    int experience = 0;

    // Start is called before the first frame update
    void Start()
    {
        PrintSplash();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnUserInput(string input)
    {
        if (input == MENU)
        {
            this.currentScreen = Screen.MainMenu;
            PrintMenu();
        }
        else if (this.currentScreen == Screen.MainMenu)
        {
            SelectLevel(input);
            GeneratePassword();
            PrintLevel();
        }
        else if (this.currentScreen == Screen.Easy)
        {
            CheckPassword(input);
        }
        else if (this.currentScreen == Screen.Medium)
        {
            CheckPassword(input);
        }
        else if (this.currentScreen == Screen.Hard)
        {
            CheckPassword(input);
        }

    }

    void SelectLevel(string level)
    {
        switch(level)
        {
            case EASY:
                this.currentScreen = Screen.Easy;
                break;
            case MEDIUM:
                this.currentScreen = Screen.Medium;
                break;
            case HARD:
                this.currentScreen = Screen.Hard;
                break;
            case "4":
                this.currentScreen = Screen.Learn;
                break;
            default:
                this.currentScreen = Screen.Error;
                break;
        }
    }
    void GeneratePassword()
    {
        string[] possiblePasswords = this.currentScreen.GetPasswords();
        int passwordIndex = UnityEngine.Random.Range(0, possiblePasswords.Length);
        this.password = possiblePasswords[passwordIndex];
    }
    void PrintLevel()
    {
        switch(this.currentScreen)
        {
            case Screen.MainMenu:
                PrintMenu();
                break;
            case Screen.Easy:
                PrintEasy();
                break;
            case Screen.Medium:
                PrintMedium();
                break;
            case Screen.Hard:
                PrintHard();
                break;
            case Screen.Learn:
            case Screen.Error:
                PrintError();
                break;
            case Screen.Win:
                PrintWin();
                break;
        }
    }

    void CheckPassword(string password)
    {
        if (this.password == password)
        {
            switch(this.currentScreen)
            {
                case Screen.Easy:
                    this.experience += 1;
                    break;
                case Screen.Medium:
                    this.experience += 2;
                    break;
                case Screen.Hard:
                    this.experience += 3;
                    break;
                default:
                    break;
            }
            this.currentScreen = Screen.Win;
            PrintLevel();
        }
        else
        {
            Terminal.WriteLine("No thats incorrect. Try again!");
        }
    }

    void PrintPasswordHint()
    {
        Terminal.WriteLine("Hint: " + password.Anagram());
        if (experience >= 10)
        {
            Terminal.WriteLine("Password starts with '" + password[0] + "'");
            Terminal.WriteLine("Password ends with '" + password[password.Length -1] + "'");
        } else if (experience >= 5)
        {
            Terminal.WriteLine("Password starts with '" + password[0] + "'");
        }
    }

    void PrintEasy()
    {
        Terminal.ClearScreen();
        PrintPasswordHint();
        Terminal.WriteLine("Enter your password guess.");
    }
    void PrintMedium()
    {
        Terminal.ClearScreen();
        PrintPasswordHint();
        Terminal.WriteLine("Enter your password guess.");
    }
    void PrintHard()
    {
        Terminal.ClearScreen();
        PrintPasswordHint();
        Terminal.WriteLine("Enter your password guess.");
    }

    private void PrintMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to the WM2000 Hacking Terminal.");
        Terminal.WriteLine("Type '" + MENU + "' anytime to go back to the menu.");
        Terminal.WriteLine("<Experience> " + experience);
        Terminal.WriteLine("What would you like to hack?");
        Terminal.WriteLine("");
        Terminal.WriteLine("("+EASY+") - Hack your parents laptop.");
        Terminal.WriteLine("("+MEDIUM+") - Hack a random warehouse.");
        Terminal.WriteLine("("+HARD+") - Hack a random IT-Sec Company.");
        //Terminal.WriteLine("(4) - Learn how to hack.");
        //Terminal.WriteLine("(5) - I'm not ready for this. (Quit)");
        Terminal.WriteLine("Enter you selection: ");
    }

    void PrintSplash()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("  _____                _            _ ");
        Terminal.WriteLine(" |_   _|              (_)          | |");
        Terminal.WriteLine("   | | __ _ __ _ _ __  _ _ _   __ _| |");
        Terminal.WriteLine("   | |/_ \\ '__| ' `  \\| | ' \\ / _` | |");
        Terminal.WriteLine("   | | __/ |  | || || | | || | (_| | |");
        Terminal.WriteLine("  _|_|\\__|_|  |_||_||_|_|_||_|\\__,_|_|");
        Terminal.WriteLine(" | |  | |          | |            ");
        Terminal.WriteLine(" | |__| | __ _  ___| | _____ _ __ ");
        Terminal.WriteLine(" |  __  |/ _` |/ __| |/ / _ \\ '__|");
        Terminal.WriteLine(" | |  | | (_| | (__|   <  __/ |   ");
        Terminal.WriteLine(" |_|  |_|\\__,_|\\___|_|\\_\\___|_|   ");
        Wait(5, PrintBoot);
    }

    void PrintBoot()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Booting...");
        Wait(1, PrintLoadingRam);
    }

    void PrintLoadingRam()
    {
        Terminal.WriteLine("Loading RAM...");
        Wait(1, PrintConnectingHackerNet);
    }
    void PrintConnectingHackerNet()
    {
        Terminal.WriteLine("");
        Terminal.WriteLine("Connecting to Hacker Net...");
        Wait(3, PrintLoadingHackingTerminal);
    }

    void PrintLoadingHackingTerminal()
    {
        Terminal.WriteLine("Connection Successful.");
        Terminal.WriteLine("Loading Hacking Terminal...");
        Wait(2, PrintWait);
    }

    void PrintWait()
    {
        Terminal.WriteLine(".");
        this.waitingCounter++;
        if (this.waitingCounter <=10)
        {
            Wait(0.5f, PrintWait);
        } else
        {
            PrintMenu();
        }
    }

    private void PrintError()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("This command is not available. Enter '0' to go back.");
    }

    void PrintWin()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("You hacked the password.");

        Wait(2, Restart);
    }

    void Restart()
    {
        this.currentScreen = Screen.MainMenu;
        PrintLevel();
    }

    void Wait(float seconds, Action action)
    {
        StartCoroutine(_wait(seconds, action));
    }

    IEnumerator _wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}

enum Screen { MainMenu, Easy, Medium, Hard, Win, Error, Learn};

static class ScreenMethods
{
    static string[] easyPasswords = { "car", "house", "kitten", "doggy", "12345", "dad", "mom" };
    static string[] mediumPasswords = { "admin", "password", "password01", "warehouse", "storage", "crates" };
    static string[] hardPasswords = { "supersecurepassword", "averyverylongsecret", "somethingdifficult", "evenmoredifficult", "somesuperduperpassword" };

    public static string[] GetPasswords(this Screen screen)
    {
        switch(screen)
        {
            case Screen.Easy:
                return easyPasswords;
            case Screen.Medium:
                return mediumPasswords;
            case Screen.Hard:
                return hardPasswords;
            default:
                return new string[] { "" };
        }
    }
}
