function prime_factors(num, primes = [])
    num <= 1 && return primes

    d = 2
    while mod(num, d) != 0
        d += 1
    end

    push!(primes, d)

    return prime_factors(num / d, primes)
end