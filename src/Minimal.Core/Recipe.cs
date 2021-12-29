


using Ardalis.GuardClauses;

public class Recipe
{
    
    public int Id { get; set; }
    public string Title { get; private set; }
    public string? Slug { get; set; }
    public string Description { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string PrepTime { get; private set; }
    public string? CookTime { get; private set; }
    public string ReadyIn { get; private set; }

    public ICollection<Step> Steps { get; set; } = new List<Step>();
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();


    public Recipe(string title, string description, string? photoUrl, string prepTime, 
            string? cookTime, string readyIn)
    {
        Title = Guard.Against.NullOrEmpty(title, nameof(title));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
        PhotoUrl = photoUrl;
        PrepTime = Guard.Against.NullOrEmpty(prepTime, nameof(prepTime));
        CookTime = cookTime;
        ReadyIn = Guard.Against.NullOrEmpty(readyIn, nameof(readyIn));
    }

    public void UpdateTitle(string newTitle)
    {
        Title = Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
    }

    public void UpdateDescription(string newDescription)
    {
        Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
    }

    public void UpdatePhotoUrl(string? newPhotoUrl)
    {
        PhotoUrl = newPhotoUrl;
    }

    public void UpdatePrepTime(string newPrepTime)
    {
        PrepTime = Guard.Against.NullOrEmpty(newPrepTime, nameof(newPrepTime));
    }

    public void UpdateCookTime(string? newCookTime)
    {
        CookTime = newCookTime;
    }

    public void UpdateReadyIn(string newReadyIn)
    {
        ReadyIn = Guard.Against.NullOrEmpty(newReadyIn, nameof(newReadyIn));
    }

    public void AddStep(Step newStep)
    {
        Steps.Add(Guard.Against.Null<Step>(newStep, nameof(newStep)));
    }

    public void AddIngredient(Ingredient newIngredient)
    {
        Ingredients.Add(Guard.Against.Null<Ingredient>(newIngredient, nameof(newIngredient)));
    }
}