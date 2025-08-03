"""
    ispangram(input)

Return `true` if `input` contains every alphabetic character (case insensitive).

"""
function ispangram(input)
    if length(input) < 26
        return false
    end
    
    uInp = uppercase(input)
    asciis = Int.(only.(split(uInp,"")))
    return issubset(65:90,asciis)
end