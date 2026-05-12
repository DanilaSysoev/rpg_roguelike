using RpgRoguelike.Core;
using RpgRoguelike.Core.Weapons;
using RpgRoguelike.Core.Weapons.Charms;
using RpgRoguelike.Persistence.Dto;
using RpgRoguelike.Services.Building.Charms;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Services.Building.Weapons;

namespace RpgRoguelike.Persistence.Json;


public class JsonGameRepository : IGameRepository
{
    public JsonGameRepository(string dataDir)
    {
        playerRepo = new JsonRepository<PlayerDto>(Path.Join(dataDir, "Players.json"));
        roomRepo = new JsonRepository<RoomDto>(Path.Join(dataDir, "Rooms.json"));
        cellRepo = new JsonRepository<CellDto>(Path.Join(dataDir, "Cells.json"));
        effectRepo = new JsonRepository<EffectDto>(Path.Join(dataDir, "Effects.json"));
        enemyRepo = new JsonRepository<EnemyDto>(Path.Join(dataDir, "Enemies.json"));
        movementStrategyRepo = new JsonRepository<MovementStrategyDto>(Path.Join(dataDir, "MovementStrategies.json"));
        rewardRepo = new JsonRepository<RewardDto>(Path.Join(dataDir, "Rewards.json"));
        weaponRepo = new JsonRepository<WeaponDto>(Path.Join(dataDir, "Weapons.json"));

        playerRepo.Load();
        roomRepo.Load();
        cellRepo.Load();
        effectRepo.Load();
        enemyRepo.Load();
        movementStrategyRepo.Load();
        rewardRepo.Load();
        weaponRepo.Load();
    }

    public Player LoadPlayer()
    {
        PlayerDto dto = playerRepo.GetAll().First();
        IWeapon? weapon = BuildWeapon(dto.WeaponId);
        return
            new PlayerBuilder()
            .SetGold(dto.Gold)
            .SetHealth(dto.Health)
            .SetMaxHealth(dto.MaxHealth)
            .SetName(dto.Name)
            .SetPosition(new Position(dto.PosLine, dto.PosCol))
            .SetWeapon(weapon)
            .Build();
    }

    private IWeapon? BuildWeapon(int weaponId)
    {
        if (weaponId == 0) return null;
        WeaponDto dto = weaponRepo.Get(weaponId);
        return weaponBuilders[dto.Type](dto, weaponRepo);
    }

    private static IWeapon BuildDagger(
        WeaponDto dto, IRepository<WeaponDto> repo
    )
    {
        return new DaggerBuilder()
            .SetBleeding(dto.BleedingValue, dto.BleedingTime)
            .SetDamage(dto.Damage)
            .SetName(dto.Name)
            .Build();
    }

    private static IWeapon BuildHammer(
        WeaponDto dto, IRepository<WeaponDto> repo
    )
    {
        return new HammerBuilder()
            .SetStunChance(dto.StunChance)
            .SetStunTime(dto.StunTime)
            .SetDamage(dto.Damage)
            .SetName(dto.Name)
            .Build();
            
    }

    private static IWeapon BuildSword(
        WeaponDto dto, IRepository<WeaponDto> repo
    )
    {
        return new SwordBuilder()
            .SetDamage(dto.Damage)
            .SetName(dto.Name)
            .Build();
    }

    private static IWeapon BuildFireCharm(
        WeaponDto dto, IRepository<WeaponDto> repo
    )
    {
        WeaponDto childDto = repo.Get(dto.ChildId);
        return new FireCharmBuilder()
            .SetBurningChance(dto.BurningChance)
            .SetBurningPower(dto.BurningPower)
            .SetBurningTime(dto.BurningTime)
            .SetFireDamage(dto.FireDamage)
            .SetWeapon(weaponBuilders[childDto.Type](childDto, repo))
            .Build();
    }

    public Room LoadRoom()
    {
        throw new NotImplementedException();
    }

    public void SavePlayer(Player player)
    {
        int weaponDtoId = SaveWeapon(player.Weapon, weaponRepo);
        PlayerDto playerDto = new PlayerDto
        {
            Gold = player.Gold,
            Name = player.Name,
            Health = player.Health,
            MaxHealth = player.MaxHealth,
            PosLine = player.Position.Line,
            PosCol = player.Position.Column,
            IsStunned = player.IsStunned,
            WeaponId = weaponDtoId,
        };
        playerRepo.Add(playerDto);

        playerRepo.Save();
        weaponRepo.Save();
    }

    private static int SaveWeapon(IWeapon? weapon, IRepository<WeaponDto> repo)
    {
        if (weapon is null)
            return 0;
        
        return weaponDtoBuilders[weapon.GetType()](weapon, repo).Id;
    }

    public void SaveRoom(Room room)
    {
        throw new NotImplementedException();
    }

    public bool CanBeLoaded()
    {
        return playerRepo.GetAll().Count() == 1;
    }

    public void LoadFinish()
    {
        playerRepo.Clear();
        roomRepo.Clear();
        cellRepo.Clear();
        effectRepo.Clear();
        enemyRepo.Clear();
        movementStrategyRepo.Clear();
        rewardRepo.Clear();
        weaponRepo.Clear();
    }

    private static WeaponDto BuildSwordDto(
        IWeapon weapon, IRepository<WeaponDto> repo
    )
    {
        Sword sword = (Sword)weapon;
        var res = new WeaponDto()
        {
            Name = sword.Name,
            Damage = sword.Damage,
            Type = "Sword",
        };
        repo.Add(res);
        return res;
    }
    private static WeaponDto BuildHammerDto(
        IWeapon weapon, IRepository<WeaponDto> repo
    )
    {
        Hammer hammer = (Hammer)weapon;
        var res = new WeaponDto()
        {
            Name = hammer.Name,
            Damage = hammer.Damage,
            StunChance = hammer.StunChance,
            StunTime = hammer.StunTime,
            Type = "Hammer",
        };
        repo.Add(res);
        return res;
    }
    private static WeaponDto BuildDaggerDto(
        IWeapon weapon, IRepository<WeaponDto> repo
    )
    {
        Dagger dagger = (Dagger)weapon;
        var res = new WeaponDto()
        {
            Name = dagger.Name,
            Damage = dagger.Damage,
            BleedingTime = dagger.BleedingTime,
            BleedingValue = dagger.BleedingValue,
            Type = "Dagger",
        };
        repo.Add(res);
        return res;
    }
    private static WeaponDto BuildFireCharmsDto(
        IWeapon weapon, IRepository<WeaponDto> repo
    )
    {
        FireCharms charm = (FireCharms)weapon;
        int childId = SaveWeapon(charm.Weapon, repo);
        var res = new WeaponDto()
        {
            FireDamage = charm.FireDamage,
            BurningChance = charm.BurningChance,
            BurningPower = charm.BurningPower,
            BurningTime = charm.BurningTime,
            ChildId = childId,
            Type = "FireCharm",
        };
        repo.Add(res);
        return res;
    }

    private readonly IRepository<PlayerDto> playerRepo;
    private readonly IRepository<RoomDto> roomRepo;
    private readonly IRepository<CellDto> cellRepo;
    private readonly IRepository<EffectDto> effectRepo;
    private readonly IRepository<EnemyDto> enemyRepo;
    private readonly IRepository<MovementStrategyDto> movementStrategyRepo;
    private readonly IRepository<RewardDto> rewardRepo;
    private readonly IRepository<WeaponDto> weaponRepo;

    private static readonly Dictionary
        <string, Func<WeaponDto, IRepository<WeaponDto>, IWeapon>>
    weaponBuilders = new()
    {
        { "Sword", BuildSword },
        { "Hammer", BuildHammer },
        { "Dagger", BuildDagger },
        { "FireCharm", BuildFireCharm },
    };

    private static readonly Dictionary
        <Type, Func<IWeapon, IRepository<WeaponDto>, WeaponDto>>
    weaponDtoBuilders = new()
    {
        { typeof(Sword), BuildSwordDto },
        { typeof(Hammer), BuildHammerDto },
        { typeof(Dagger), BuildDaggerDto },
        { typeof(FireCharms), BuildFireCharmsDto },
    };
}
