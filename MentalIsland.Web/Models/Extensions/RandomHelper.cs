namespace MentalIsland.Web.Models.Extensions;

public static class RandomHelper
{
    private static Random random;

    public static Random GetRandom()
    {
        if (random == null)
            random = new Random();

        return random;
    }

}