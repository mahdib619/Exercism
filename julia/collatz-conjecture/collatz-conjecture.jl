function collatz_steps(n)
    n <= 0 && throw(DomainError(""))

    c = 0
    while n != 1
        n = iseven(n) ? n / 2 : 3n + 1
        c += 1
    end

    return c
end