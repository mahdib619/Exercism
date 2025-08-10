function aliquot_sum(num)
	num <= 0 && throw(DomainError(""))

    a, b, divisors = 1, num, Int[]
    while b > a
        mod(b, 1) == 0 && push!(divisors, a, b)
        a += 1
        b = num / a
    end
    sum(divisors) - num
end

isperfect(num) = num == aliquot_sum(num)
isabundant(num) = num < aliquot_sum(num)
isdeficient(num) = num > aliquot_sum(num)