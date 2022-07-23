struct ComplexNumber <: Number
    rp::Float64
    ip::Float64
end

ComplexNumber(rp) = ComplexNumber(rp, 0)
const jm = ComplexNumber(0, 1)

real(c) = c.rp
imag(c) = c.ip

Base.:+(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) + real(c2), imag(c1) + imag(c2))
Base.:+(c::ComplexNumber, n::Real) = c + ComplexNumber(n)
Base.:+(n::Real, c::ComplexNumber) = c + n
Base.:-(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) - real(c2), imag(c1) - imag(c2))
Base.:*(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) * real(c2) - imag(c1) * imag(c2), real(c1) * imag(c2) + imag(c1) * real(c2))
Base.:*(c::ComplexNumber, n::Real) = c * ComplexNumber(n)
Base.:*(n::Real, c::ComplexNumber) = ComplexNumber(n) * c
Base.:/(c1::ComplexNumber, c2::ComplexNumber) = (de = real(c2)^2 + imag(c2)^2; ComplexNumber((real(c1) * real(c2) + imag(c1) * imag(c2)) / de, (imag(c1) * real(c2) - real(c1) * imag(c2)) / de))
Base.:^(c::ComplexNumber, p::Int) = reduce(*, repeat([c], mod(p, 4)))
Base.:≈(c1::ComplexNumber, c2::ComplexNumber) = isapprox(real(c1), real(c2), atol=eps() * 2) && isapprox(imag(c1), imag(c2), atol=eps() * 2)

Base.abs(c::ComplexNumber) = √(real(c)^2 + imag(c)^2)
Base.conj(c::ComplexNumber) = ComplexNumber(real(c), (i = imag(c); i == 0 ? 0 : -i))
Base.exp(c::ComplexNumber) = ComplexNumber(ℯ^real(c)) * ComplexNumber(cos(imag(c)), sin(imag(c)))