using static System.Net.Mime.MediaTypeNames;
using System;

public class Item
{
    public string Name { get; }
    public int Value { get; }

    public Item(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

public class Weapon : Item
{
    public int Damage { get; }

    public Weapon(string name, int value, int damage) : base(name, value)
    {
        Damage = damage;
    }
}

public class Support : Item
{
    public int Effect { get; }

    public Support(string name, int value, int effect) : base(name, value)
    {
        Effect = effect;
    }
    public virtual void ApplyEffect(Entity player)
    {

    }
}
public class HealthPotion : Support
{
    public HealthPotion() : base("Health potion", 15, 35)
    {

    }
    public void HealthPotionEffect(Entity player)
    {
        player.RestoreHealth(35);
    }
}

public class ManaPotion : Support
{
    public ManaPotion() : base("Mana potion", 15, 20)
    {

    }
    public void ManaPotionEffect(Entity player)
    {
        player.RestoreMana(35);
    }
}

public class DefencePotion : Support
{
    public DefencePotion() : base("Defence potion", 25, 5)
    {

    }
    public void DefencePotionEffect(Entity player)
    {
        player.BuffDefence(5);
    }
}

public class Armor : Item
{
    public int Defence { get; }
    public int MagicDefence { get; }

    public Armor(string name, int value, int def, int mdef) : base(name, value)
    {
        Defence = def;
        MagicDefence = mdef;
    }
}

public class ShortSword : Weapon
{
    public ShortSword() : base("Shortsword", 15, new Random().Next(5, 11))
    {

    }
}

public class LongSword : Weapon
{
    public LongSword() : base("Longsword", 65, new Random().Next(9, 16))
    {

    }
}

public class QualitySword : Weapon
{
    public QualitySword() : base("Quality sword", 65, new Random().Next(1, 11))
    {

    }
    public void QualitySwordAbility(Warrior warrior, int playerTurn)
    {
        playerTurn += 1;
    }
}

public class MagicStick : Weapon
{
    public MagicStick() : base("Magical stick", 7, new Random().Next(1, 16))
    {

    }
}

public class WizardsStaff : Weapon
{
    public WizardsStaff() : base("Wizard's staff", 76, new Random().Next(5, 21))
    {

    }
    public void WizardStaffAbility(Mage mage, int manaCost)
    {
        manaCost -= 5;
    }
}

public class FireStaff : Weapon
{
    public FireStaff() : base("Fire staff", 76, new Random().Next(5, 21))
    {
    
    }
    public void FireStaffAbility(Mage mage, int firedamage)
    {
        firedamage += new Random().Next(5, 11);
    }
}

public class BasicBow : Weapon
{
    public BasicBow() : base("Basic bow", 10, new Random().Next(3, 6))
    {

    }
}

public class LongBow : Weapon
{
    public LongBow() : base("Shortsword", 55, new Random().Next(5, 16))
    {

    }
}

public class ShortBow : Weapon
{
    public ShortBow() : base("Short bow", 55, new Random().Next(1, 11))
    {
        
    }
    public void ShortBowAbility(int playerTurn)
    {
        playerTurn += 1;
    }
}

public class WarriorArmor : Armor
{
    public WarriorArmor() : base("Warrior armor", 150, 15, 5)
    {

    }
}

public class MageRobe : Armor
{
    public MageRobe() : base("Mage robe", 125, 5, 20)
    {

    }
}

public class LeatherArmor : Armor
{
    public LeatherArmor() : base("Leather armor", 100, 10, 10)
    {

    }
}

public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Inventory:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} - Value: {item.Value}");
        }
    }
    public bool HasSupportItems(Item item)
    {
        return items.Any(item => item is Support);
    }

    public void DisplaySupportItems()
    {
        Console.WriteLine("Support Items:");
        int index = 1;
        foreach (var item in items)
        {
            if (item is Support)
            {
                Console.WriteLine($"{index}. {item.Name}");
                index++;
            }
        }
    }
    public Support GetSupportItem(int index)
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item is Support)
            {
                if (count == index)
                {
                    return (Support)item;
                }
                count++;
            }
        }
        return null;
    }
}

public class Entity
{
    public int hp, def, mana;

    public Entity(int hp, int def, int mana)
    {
        this.hp = hp;
        this.def = def;
        this.mana = mana;
        Inventory inventory = new Inventory();
    }
    public void RestoreHealth(int amount)
    {
        hp += amount;
    }

    public void RestoreMana(int amount)
    {
        mana += amount;
    }

    public void BuffDefence(int amount)
    {
        def += amount;
    }
}

public class Human : Entity
{
    public Inventory Inventory { get; set; }
    public Human(int hp, int def, int mana) : base(hp, def, mana)
    {
        this.inventory = new Inventory(); 
    }
}

public class Orc : Entity
{
    public Orc(int hp, int def, int mana) : base(hp, def, mana)
    {

    }
    public void OrcAbility(double orcSmash, double damage, int mana)
    {
        orcSmash = damage + (damage * 0.50) + (mana - 45);
    }
}

public class Goblin : Entity
{
    public Goblin(int hp, int def, int mana) : base(hp, def, mana)
    {

    }
}

public class Demon : Entity
{
    public Demon(int hp, int def, int mana) : base(hp, def, mana)
    {

    }
    public void DemonAbility(int mana)
    {
        int fireBreath;

        fireBreath = new Random().Next(10, 36) + (mana - 60);
    }
}

public class Warrior
{
    public int hp { get; set; }
    public int def { get; set; }
    public int mdef { get; set; }
    public int mana {  get; set; }

    public Warrior(int hp, int def, int mdef, int mana)
    {
        hp = hp;
        def = def;
        mdef = mdef;
        mana = mana;
        Inventory inventory = new Inventory();
    }
}


public class Mage
{
    Warrior ddwarrior = new Warrior(100, 50,40, 1000);
    
    int hp, def, mdef, mana;


    public Mage(int hp, int def, int mdef, int mana)
    {
        this.hp = hp;
        this.def = def;
        this.mdef = mdef;
        this.mana = mana;
        Inventory inventory = new Inventory();
    }
}

public class Archer
{
    public bool CanUseQuickAttack { get; private set; } = true;
    private int turnsUntilQuickAttackAvailable = 3;

    int hp, def, mdef, mana;

    public Archer(int hp, int def, int mdef, int mana)
    {
        this.hp = hp;
        this.def = def;
        this.mdef = mdef;
        this.mana = mana;
        Inventory inventory = new Inventory();
    }
    // Method to decrement turns until Quick Attack is available and reset CanUseQuickAttack
    public void UpdateTurnsUntilQuickAttack()
    {
        turnsUntilQuickAttackAvailable--;
        if (turnsUntilQuickAttackAvailable <= 0)
        {
            CanUseQuickAttack = true;
            turnsUntilQuickAttackAvailable = 3; // Reset the turns until Quick Attack is available again
        }
    }
}

public static class CombatManager
{
    public static void InflictDamage(Entity target, int damage, int def, int hp)
    {
        int damageAfterArmor = damage - target.def;

        damageAfterArmor = Math.Max(0, damageAfterArmor);

        if (target.hp - damageAfterArmor < 0)
        {
            target.hp = 0;
        }
        else
        {
            target.hp -= damageAfterArmor;
        }
    }

    public static void StartCombat(Entity player, List<Entity> enemies)
    {
        Console.WriteLine("Combat started!");

        bool combatEnd = false;

        while (!combatEnd)
        {
            static void PlayerTurn(Entity player, List<Entity> enemies, int playerChoice, int damage, int manaCost, int mana, int hp);

            if (ShouldEndCombat(player, enemies))
            {
                combatEnd = true;
                break;
            }

            foreach (var enemy in enemies)
            {
                EnemyTurn(enemy, player);

                if (ShouldEndCombat(player, enemies))
                {
                    combatEnd = true;
                    break;
                }
            }
        }

        EndCombat(player, enemies);

        while (player.hp > 0)
        {
            // Perform combat actions...

            if (ShouldEndCombat(player, enemies))
            {
                break;
            }
        }
        EndCombat(player, enemies);
    }
    public static void PlayerTurn(Entity player, List<Entity> enemies, int playerChoice, int damage, int manaCost, int mana, int hp, bool CanUseQuickAttack)
    {
        

        Console.WriteLine("Player's Turn");

        if (player.inventory.HasSupportItems())
        {
            Console.WriteLine("Choose an action: 1) Attack 2) Use Support Item");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 2)
            {
                Console.WriteLine("Select a support item to use:");
                player.Inventory.DisplaySupportItems();
                int itemIndex = int.Parse(Console.ReadLine()) - 1; // Assuming 1-based indexing
                Support selectedSupport = player.Inventory.GetSupportItem(itemIndex);

                // Apply the effect of the support item
                selectedSupport.ApplyEffect(player);
                player.Inventory.RemoveItem(selectedSupport); // Remove the used support item from inventory
            }
        }
        //Class choices
        if (player is Warrior)
        {
            Console.WriteLine("Choose an ability! 1) Backslash(+3 attack damage 2) Double slash (Attacks twice 3) Overpower (+10 attack damage)");
            playerChoice = int.Parse(Console.ReadLine());

        }
        else if (player is Mage)
        {
            Console.WriteLine("Choose an ability! 4) Arcane strike (+5 magic damage) 5) Firebolt (+10 fire damage) 6) Mystic heal (Heals 30 Hp)");
            playerChoice = int.Parse(Console.ReadLine());
        }
        else if (player is Archer)
        {
            Console.WriteLine("Choose an ability! 7) Precision strike (+1 attack damage) 8) Bleed (30% DoT for 3 turns) 9) Quick attack (A single attack that does not consume a turn)");
            playerChoice = int.Parse(Console.ReadLine());
        }

        //Ability choices (Warrior)
        if (playerChoice == 1)
        {
            damage += 3;
            manaCost = mana -= 5;
        }
        else if (playerChoice == 2)
        {
            damage *= 2;
            manaCost = mana -= 20;
        }
        else if (playerChoice == 3)
        {
            damage += 10;
            manaCost = mana -= 30;
        }

        //Ability choices (Mage)
        if (playerChoice == 4)
        {
            damage += 5;
            manaCost = mana -= 10;
        }
        else if (playerChoice == 5)
        {
            damage += 10;
            manaCost = mana -= 15;
        }
        else if (playerChoice == 6)
        {
            hp += 30;
            manaCost = mana -= 40;
        }

        //Ability choices (Archer)
        if (playerChoice == 7)
        {
            damage += 1;
            manaCost = mana -= 5;
        }
        else if (playerChoice == 8)
        {
            damage += (damage * 2);
            manaCost = mana -= 15;
        }
        else if (playerChoice == 9)
        {
            if (playerChoice == 9 && ((Archer)player).CanUseQuickAttack)
            {
                damage = 10;
                ((Archer)player).CanUseQuickAttack = false;
                ((Archer)player).UpdateTurnsUntilQuickAttack(); // Update turns until Quick Attack is available again
            }
            else
            {
                Console.WriteLine("Quick Attack is not available yet. Choose another ability.");
                return;
            }
            if (!usedQuickAttack)
            {
                playerTurn += 1;
                manaCost = mana -= 25;
            }

        }
        Console.Clear();
    }
    public static void EnemyTurn(Entity enemy, Entity player, int damage)
    {
        Console.WriteLine($"Enemy's Turn: {enemy.GetType().Name}");

        if (enemy is Goblin)
        {
            damage *= 2;
        }
        if (enemy is Orc)
        {
            ((Orc)enemy).OrcAbility();
        }
        if (enemy is Demon)
        {
            ((Demon)enemy).DemonAbility();
        }
    }

    public static bool ShouldEndCombat(Entity player, List<Entity> enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy.hp > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public static void EndCombat(Entity player, List<Entity> enemies)
    {
        ResetPlayerState(player);
        ResetEnemyState(enemies);
    }

    public static bool AddGold(int amount, int goldCoins)
    {
        goldCoins += amount;
        return true;
    }

    public static bool RemoveGold(int amount, int goldCoins)
    {
        goldCoins -= amount;
        goldCoins = Math.Max(goldCoins, 0);
        return true;
    }
}

public class Room
{
    public string Name { get; }
    public string Description { get; }
    public List<Item> Items { get; }

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
        Items = new List<Item>();
    }
    public void AddItem(Item item)
    {
        Item.(item);
    }
    public void RemoveItem(Item item)
    {
        Item.Remove(item);
    }
    public static bool RemoveGold(int amount, int goldCoins)
    {
        goldCoins -= amount;
        goldCoins = Math.Max(goldCoins, 0);
        return true;
    }

    public void DisplayItems()
    {
        Console.WriteLine("Items available in the room:");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Name} - Value: {item.Value}");
        }
    }
}

public class City
{
    private Room currentRoom;

    public Room CurrentRoom
    {
        get { return currentRoom; }
        set { currentRoom = value; }
    }

    public City()
    {
        Room plaza = new Room("Plaza", "You are in the central plaza of the city.");
        plaza.AddItem(new HealthPotion());
        plaza.AddItem(new ManaPotion());
        plaza.AddItem(new DefencePotion());
        plaza.AddItem(new ShortSword());
        plaza.AddItem(new LongSword());
        plaza.AddItem(new QualitySword());
        plaza.AddItem(new WarriorArmor());
        plaza.AddItem(new MagicStick());
        plaza.AddItem(new WizardsStaff());
        plaza.AddItem(new FireStaff());
        plaza.AddItem(new MageRobe());
        plaza.AddItem(new BasicBow());
        plaza.AddItem(new LongBow());
        plaza.AddItem(new ShortBow());
        plaza.AddItem(new LeatherArmor());
        Room trainingArea = new Room("Training Area", "You are in the training area.");
        Room outerGates = new Room("Outer Gates", "You are at the outer gates of the city.");

        // Connect rooms
        plaza.ConnectRoom("north", trainingArea);
        plaza.ConnectRoom("east", outerGates);
        outerGates.AddEntity(new Orc(160, 20, 70)); // HP, Defense, Mana
        outerGates.AddEntity(new Goblin(50, 5, 10)); // HP, Defense, Mana
        outerGates.AddEntity(new Demon(250, 30, 160)); // HP, Defense, Mana
        // Set starting room
        currentRoom = plaza;
    }

    public void MoveToRoom(string direction)
    {
        Room nextRoom = currentRoom.GetConnectedRoom(direction);
        if (nextRoom != null)
        {
            currentRoom = nextRoom;
            DisplayRoomInformation();
        }
        else
        {
            Console.WriteLine("There is no path in that direction.");
        }
    }

    private void DisplayRoomInformation()
    {
        Console.WriteLine($"Current Location: {currentRoom.Name}");
        Console.WriteLine(currentRoom.Description);
    }
}

public static class DirectionExtensions
{
    public static void ConnectRoom(this Room room, string direction, Room connectedRoom)
    {
        switch (direction)
        {
            case "north":
                room.North = connectedRoom;
                break;
            case "south":
                room.South = connectedRoom;
                break;
            case "east":
                room.East = connectedRoom;
                break;
            case "west":
                room.West = connectedRoom;
                break;
            
            default:
                throw new ArgumentException("Invalid direction.");
        }
    }

    public static Room GetConnectedRoom(this Room room, string direction)
    {
        switch (direction)
        {
            case "north":
                return room.North;
            case "south":
                return room.South;
            case "east":
                return room.East;
            case "west":
                return room.West;
            // Add more directions as needed
            default:
                return null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the RPG game!");

        Console.WriteLine("Choose your class:");
        Console.WriteLine("1. Warrior");
        Console.WriteLine("2. Mage");
        Console.WriteLine("3. Archer");
        Console.Write("Enter the number of the class of your choice: ");

        int op;
        while (!int.TryParse(Console.ReadLine(), out op) || op < 1 || op > 3)
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
            Console.Write("Enter the number of your choice: ");
        }

        Human player;
        switch (op)
        {
            case 1:
                player = CreateWarrior();
                break;
            case 2:
                player = CreateMage();
                break;
            case 3:
                player = CreateArcher();
                break;
            default:
                throw new InvalidOperationException("Invalid choice.");
        }

        if (City.CurrentRoom.Name == "Plaza")
        {
            player = Console.ReadLine();
        }
        else if (player == Item)
        {
            Room.RemoveGold();
        }

        City city = new City();
        city.DisplayRoomInformation();


        Console.Write("Enter direction (north, south, east, west): ");
        string direction = Console.ReadLine().ToLower();
        city.MoveToRoom(direction);

        if (city.CurrentRoom.Name == "Outer Gates")
        {
            Goblin goblin = new Goblin();
            Demon demon = new Demon();
            Orc orc = new Orc();

            CombatManager.StartCombat(player, monsters);
            CombatManager.InflictDamage(player, enemyDamage);
            CombatManager.PlayerTurn(player, enemies);
            CombatManager.EnemyTurn(enemy, player);
            bool combatEnd = CombatManager.ShouldEndCombat(player, enemies);
            CombatManager.EndCombat(player, enemies);
            CombatManager.AddGold();
        }

        Console.WriteLine("Your character:");
        DisplayPlayerInfo(player);

        Console.WriteLine("Your Inventory:");
        player.Inventory.DisplayInventory();
    }

    static Warrior CreateWarrior()
    {
        Console.WriteLine("Creating a Warrior character...");
        return new Warrior(125, 10, 0, 70);
    }

    static Mage CreateMage()
    {
        Console.WriteLine("Creating a Mage character...");
        return new Mage(65, 0, 15, 120);
    }

    static Archer CreateArcher()
    {
        Console.WriteLine("Creating an Archer character...");
        return new Archer(80, 5, 5, 50);
    }

    static void DisplayPlayerInfo(Human player)
    {
        Console.WriteLine($"Hp: {player.hp}, Def: {player.def}, Mdef: {player.mdef}, Mana: {player.mana}");
    }
}
