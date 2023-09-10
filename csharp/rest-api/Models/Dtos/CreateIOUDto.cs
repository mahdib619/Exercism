namespace RestApiExercise.Models.Dtos;

internal class CreateIOUDto
{
    public string Lender { get; init; }
    public string Borrower { get; init; }
    public int Amount { get; init; }
}
