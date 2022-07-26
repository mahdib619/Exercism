function clean(phone_number)
    !occursin(r"^ *\+?1? *([ \(-\.]*[2-9]\d\d[ \)-\.]*){2}\d{4} *$", phone_number) && throw(ArgumentError(""))
    replace(phone_number, r"(?!\d)." => "")[(end-9):end]
end