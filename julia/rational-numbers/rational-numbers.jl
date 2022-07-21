struct RationalNumber{Int} <: Real
    num::Int
    denom::Int

    function RationalNumber(n::Int, d::Int=1)
        n == d == 0 && throw(ArgumentError(""))
        return new{Int}(div.(flipsign.((n, d), d), gcd(n,d))...)
    end
end

numerator(a::RationalNumber) = a.num
denominator(a::RationalNumber) = a.denom

Base.Float64(a::RationalNumber) = a.num / a.denom
Base.promote_type(::Type{RationalNumber{Int}}, ::Type{<:Real}) = Float64
Base.promote_type(::Type{<:Real}, ::Type{RationalNumber{Int}}) = Float64

Base.inv(a::RationalNumber) = RationalNumber(a.denom, a.num)
Base.zero(::Type{RationalNumber{Int}}) = RationalNumber(0, 1)
Base.one(::Type{RationalNumber{Int}}) = RationalNumber(1, 1)
Base.abs(a::RationalNumber) = RationalNumber(abs(a.num), a.denom)

Base.:+(a::RationalNumber, b::RationalNumber) = RationalNumber(a.num * b.denom + b.num * a.denom, a.denom * b.denom)
Base.:*(a::RationalNumber, b::RationalNumber) = RationalNumber(a.num * b.num, a.denom * b.denom)
Base.:*(a::RationalNumber, b::Int) = a * RationalNumber(b)
Base.:-(a::RationalNumber, b::RationalNumber) = a + b * -1
Base.:/(a::RationalNumber, b::RationalNumber) = a * inv(b)
Base.:^(a::RationalNumber, p::Int) = RationalNumber(a.num^p, a.denom^p)
Base.:^(a::Int, p::RationalNumber) = (a^Float64(p.num))^inv(Float64(p.denom))

Base.:>(a::RationalNumber, b::RationalNumber) = Float64(a) > Float64(b)
Base.:<(a::RationalNumber, b::RationalNumber) = Float64(a) < Float64(b)
Base.:<=(a::RationalNumber, b::RationalNumber) = a < b || a == b
Base.:>=(a::RationalNumber, b::RationalNumber) = a > b || a == b

Base.show(io::IO, a::RationalNumber) = print(io, "$(a.num)//$(a.denom)")