allergies = Dict(0 => "eggs", 1 => "peanuts", 2 => "shellfish", 3 => "strawberries", 4 => "tomatoes", 5 => "chocolate", 6 => "pollen", 7 => "cats")

allergic_to(score, allergen) = allergen in (allergy_list(score))
allergy_list(score) = allergy_list(score, Set())

function allergy_list(score, list)
    score == 0 && return list
    score == 1 && return push!(list, allergies[0])
	
    l = trunc(Int, log2(score))
    haskey(allergies, l) && push!(list, allergies[l])
    allergy_list(score - 2^l, list)
end