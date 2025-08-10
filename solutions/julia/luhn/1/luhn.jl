function luhn(input)
    !occursin(r"^( ?\d){2,}$", input) && return false

    nums = parse.(Int, split(replace(input, r"(?!\d).{1}" => ""), ""))
    for i in length(nums)-1:-2:1
        m = nums[i] * 2
        nums[i] = m > 9 ? m - 9 : m
    end

    mod(sum(nums), 10) == 0
end