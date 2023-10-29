using System.Runtime.CompilerServices;

namespace JacksonVeroneze.TemplateWebApi.Domain.Util;

public class ValidarCpf
{
    public static bool Validar(string? sourceCpf)
    {
        if (string.IsNullOrWhiteSpace(sourceCpf))
            return false;

        string clearCpf = sourceCpf.Trim();
        clearCpf = clearCpf.Replace("-", "");
        clearCpf = clearCpf.Replace(".", "");

        if (clearCpf.Length != 11)
        {
            return false;
        }

        int totalDigitoI = 0;
        int totalDigitoII = 0;

        if (clearCpf.Equals("00000000000") ||
            clearCpf.Equals("11111111111") ||
            clearCpf.Equals("22222222222") ||
            clearCpf.Equals("33333333333") ||
            clearCpf.Equals("44444444444") ||
            clearCpf.Equals("55555555555") ||
            clearCpf.Equals("66666666666") ||
            clearCpf.Equals("77777777777") ||
            clearCpf.Equals("88888888888") ||
            clearCpf.Equals("99999999999"))
        {
            return false;
        }

        if (clearCpf.Any(c => !char.IsNumber(c)))
        {
            return false;
        }

        for (int posicao = 0; posicao < clearCpf.Length - 2; posicao++)
        {
            totalDigitoI += ObterDigito(clearCpf, posicao) * (10 - posicao);
            totalDigitoII += ObterDigito(clearCpf, posicao) * (11 - posicao);
        }

        int modI = totalDigitoI % 11;
        if (modI < 2) { modI = 0; }
        else { modI = 11 - modI; }

        if (ObterDigito(clearCpf, 9) != modI)
        {
            return false;
        }

        totalDigitoII += modI * 2;
        int mod11 = totalDigitoII % 11;
        if (mod11 < 2) { mod11 = 0; }
        else { mod11 = 11 - mod11; }

        return ObterDigito(clearCpf, 10) == mod11;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ObterDigito(
        string value,
        int pos
    ) => value[pos] - '0';
}
