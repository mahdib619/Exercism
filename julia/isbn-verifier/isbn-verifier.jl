struct ISBN
    value::AbstractString

    function ISBN(str)
        str = replace(str, "-" => "")
        !occursin(r"^\d{9}(\d|X)$", str) && throw(DomainError(""))

        nums = split(str, "")
        cd = pop!(nums)
        sumN = sum([n * m for (n, m) in zip(parse.(Int, nums), 10:-1:2)]) + (cd == "X" ? 10 : parse(Int, cd))

        mod(sumN, 11) != 0 && throw(DomainError(""))

        return new(str)
    end
end

Base.isequal(x::ISBN, y::ISBN) = x.value == y.value