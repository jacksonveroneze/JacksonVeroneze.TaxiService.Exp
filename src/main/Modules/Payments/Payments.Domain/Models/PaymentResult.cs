namespace TaxiService.Modules.Payments.Domain.Models;

public record PaymentResult
{
    public bool Success { get; }
    public string? SignatureId { get; }
    public string? ErrorMessage { get; }

    private PaymentResult(
        bool success,
        string? signatureId,
        string? errorMessage = null)
    {
        Success = success;
        SignatureId = signatureId;
        ErrorMessage = errorMessage;
    }

    public static PaymentResult Build(
        bool isSuccess, string? signatureId, 
        string? errorMessage) =>
        new(isSuccess, signatureId, errorMessage);

    public static PaymentResult WithError(
        string errorMessage) =>
        new(false, null, errorMessage);
}