function to_roman(number)
    number <= 0 && throw(ErrorException(""))
    global rdigits = Dict(1000 => 'M', 500 => 'd', 100 => 'C', 50 => 'l', 10 => 'X', 5 => 'v', 1 => 'I')
    return to_roman(number, "")
end

function to_roman(number, cr)
    number == 0 && return cr

    for k in sort(collect(keys(rdigits)); rev=true)
        n, d = number - k, rdigits[k]

        n >= 0 && return to_roman(n, cr * uppercase(d))

        pop!(rdigits, k)

        behindval = k / (islowercase(d) ? 5 : 10)
        behindval >= abs(n) && return to_roman(n + behindval, cr * rdigits[behindval] * uppercase(d))
    end
end