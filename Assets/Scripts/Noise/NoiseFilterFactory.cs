

public static class NoiseFilterFactory
{
    public static INoiseFilter GetNoiseFilterFromNoiseSettings(NoiseSettings noiseSettings)
    {
        switch (noiseSettings.filterType)
        {
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(noiseSettings);
            case NoiseSettings.FilterType.Rigid:
                return new RigidNoiseFilter(noiseSettings);
            default:
                return null;
        }
    }
}
