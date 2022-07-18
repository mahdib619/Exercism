function luhn(input)
    !occursin(r"^( ?\d){2,}$", input) && return false

    nums = parse.(Int, split(replace(input, r"(?!\d).{1}" => ""), ""))
    nums[end-1:-2:begin] = nums[end-1:-2:begin] .|> n -> (n = n * 2; n > 9 ? n - 9 : n)

    mod(sum(nums), 10) == 0
end