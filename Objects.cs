namespace Gravity_Simulation
{
    public static class Objects
    {
        public static readonly List<SpaceObject> objects = [];

        // Массы небесных тел
        public const double SunMass = 1.989e30; // Солнце
        public const double EarthMass = 5.972e24; // Земля
        public const double MoonMass = 7.347673e22; // Луна
        public const double MercuryMass = 3.3011e23; // Меркурий
        public const double VenusMass = 4.867e24; // Венера
        public const double MarsMass = 6.4171e23; // Марс
        public const double JupiterMass = 1.898e27; // Юпитер
        public const double SaturnMass = 5.683e26; // Сатурн
        public const double UranusMass = 8.681e25; // Уран
        public const double NeptuneMass = 1.024e26; // Нептун

        // Радиусы небесных тел
        public const double SunRadius = 6.957e8; // Радиус Солнца
        public const double EarthRadius = 6371e3; // Радиус Земли
        public const double MoonRadius = 1737.4e3; // Радиус Луны
        public const double MercuryRadius = 2.4397e6; // Радиус Меркурия
        public const double VenusRadius = 6051.8e3; // Радиус Венеры
        public const double MarsRadius = 3389.5e3; // Радиус Марса
        public const double JupiterRadius = 69911e3; // Радиус Юпитера
        public const double SaturnRadius = 58232e3; // Радиус Сатурна
        public const double UranusRadius = 25362e3; // Радиус Урана
        public const double NeptuneRadius = 2.4622e7; // Радиус Нептуна

        // Орбитальные дистанции
        public const double SunMercuryDistance = 5.791e10; // Расстояние до Меркурия
        public const double SunVenusDistance = 1.082e11; // Расстояние до Венеры
        public const double EarthSunDistance = 1.496e11; // Расстояние до Земли
        public const double SunMarsDistance = 2.2794e11; // Расстояние до Марса
        public const double SunJupiterDistance = 7.7833e11; // Расстояние до Юпитера
        public const double SunSaturnDistance = 1.4294e12; // Расстояние до Сатурна
        public const double SunUranusDistance = 2.87099e12; // Расстояние до Урана
        public const double SunNeptuneDistance = 4.504e12; // Расстояние до Нептуна
        public const double MoonEarthDistance = 384400e3; // Расстояние до Луны от Земли

        // Орбитальные скорости
        public const double MercuryOrbitalSpeed = 47870; // Орбитальная скорость Меркурия
        public const double VenusOrbitalSpeed = 35020; // Орбитальная скорость Венеры
        public const double EarthOrbitalSpeed = 29783; // Орбитальная скорость Земли
        public const double MoonOrbitalSpeed = 1022; // Орбитальная скорость Луны
        public const double MarsOrbitalSpeed = 24077; // Орбитальная скорость Марса
        public const double JupiterOrbitalSpeed = 13070; // Орбитальная скорость Юпитера
        public const double SaturnOrbitalSpeed = 9680; // Орбитальная скорость Сатурна
        public const double UranusOrbitalSpeed = 6820; // Орбитальная скорость Урана
        public const double NeptuneOrbitalSpeed = 5500; // Орбитальная скорость Нептуна

        static Objects()
        {
            objects = [
    new SpaceObject(
        name: "sun",
        pos: new Vector(0, 0),
        inertia: new Vector(0, 0),
        mass: SunMass,
        radius: SunRadius
    ),

    new SpaceObject(
        name: "mercury",
        pos: new Vector(SunMercuryDistance, 0),
        inertia: new Vector(0, MercuryOrbitalSpeed),
        mass: MercuryMass,
        radius: MercuryRadius
    ),

    new SpaceObject(
        name: "venus",
        pos: new Vector(SunVenusDistance, 0),
        inertia: new Vector(0, VenusOrbitalSpeed),
        mass: VenusMass,
        radius: VenusRadius
    ),

    new SpaceObject(
        name: "earth",
        pos: new Vector(EarthSunDistance, 0),
        inertia: new Vector(0, EarthOrbitalSpeed),
        mass: EarthMass,
        radius: EarthRadius
    ),

    new SpaceObject(
        name: "moon",
        pos: new Vector(EarthSunDistance + MoonEarthDistance, 0),
        inertia: new Vector(0, EarthOrbitalSpeed + MoonOrbitalSpeed),
        mass: MoonMass,
        radius: MoonRadius
    ),

    new SpaceObject(
        name: "mars",
        pos: new Vector(SunMarsDistance, 0),
        inertia: new Vector(0, MarsOrbitalSpeed),
        mass: MarsMass,
        radius: MarsRadius
    ),

    new SpaceObject(
        name: "jupiter",
        pos: new Vector(SunJupiterDistance, 0),
        inertia: new Vector(0, JupiterOrbitalSpeed),
        mass: JupiterMass,
        radius: JupiterRadius
    ),
    new SpaceObject(
        name: "saturn",
        pos: new Vector(SunSaturnDistance, 0),
        inertia: new Vector(0, SaturnOrbitalSpeed),
        mass: SaturnMass,
        radius: SaturnRadius
    ),

    new SpaceObject(
        name: "uranus",
        pos: new Vector(SunUranusDistance, 0),
        inertia: new Vector(0, UranusOrbitalSpeed),
        mass: UranusMass,
        radius: UranusRadius
    ),


    new SpaceObject(
        name: "neptune",
        pos: new Vector(SunNeptuneDistance, 0),
        inertia: new Vector(0, NeptuneOrbitalSpeed),
        mass: NeptuneMass,
        radius: NeptuneRadius
    ),
];
        }
    }
}