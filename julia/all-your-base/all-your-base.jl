function all_your_base(digits, base_in, base_out)
    (base_in <= 1 || base_out <= 1) && throw(DomainError(""))
    isempty(digits) && push!(digits, 0)

    base10 = sum(map(it -> ((it[1]>=base_in || it[1] < 0) && throw(DomainError("")); it[1] * base_in^it[2]), zip(digits, length(digits)-1:-1:0)))

    out = Int[]
    while true
        push!(out, mod(base10, base_out))
        base10 = trunc(Int, base10 / base_out)

		base10 <= 0 && break
    end

	return reverse(out)
end