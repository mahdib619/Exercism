words = Dict(3=>"Pling",5=>"Plang",7=>"Plong")
getword(num,factor) = mod(num,factor)==0 ? words[factor] :  ""
function raindrops(number)
    output = "$(getword(number,3))$(getword(number,5))$(getword(number,7))"
    length(output) == 0 ? string(number) : output
end
