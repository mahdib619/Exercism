struct ComplexNumber <: Number
    rp::Float64
    ip::Float64
end

ComplexNumber(rp) = ComplexNumber(rp, 0)

real(c) = c.rp
imag(c) = c.ip

Base.:+(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) + real(c2), imag(c1) + imag(c2))
Base.:-(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) - real(c2), imag(c1) - imag(c2))
Base.:*(c1::ComplexNumber, c2::ComplexNumber) = ComplexNumber(real(c1) * real(c2) - imag(c1) * imag(c2), real(c1) * imag(c2) + imag(c1) * real(c2))
Base.:/(c1::ComplexNumber, c2::ComplexNumber) = (de = real(c2)^2 + imag(c2)^2; ComplexNumber((real(c1) * real(c2) + imag(c1) * imag(c2)) / de, (imag(c1) * real(c2) - real(c1) * imag(c2)) / de))
Base.:^(c::ComplexNumber, p::Int) = reduce(*, repeat([c], mod(p, 4)))
Base.:≈(c1::ComplexNumber, c2::ComplexNumber) = real(c1) ≈ real(c2) && imag(c1) ≈ imag(c2)

abs(c::ComplexNumber) = √(real(c)^2 + imag(c)^2)
conj(c::ComplexNumber) = ComplexNumber(real(c), (i = imag(c); i == 0 ? 0 : -i))