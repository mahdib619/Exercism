tested_benus_tasks = (rev=true, by=true, lt=true, multiple_matches=true)

function binarysearch(arr, val; rev=false, by=identity, lt=<)
    val = by(val)
    min, max, mid = 1, length(arr), 0
    lt = rev ? (lt |> !) : lt

    while min <= max
        mid = trunc(Int, (min + max) / 2)
        midv = by(arr[mid])
		
        if midv == val
            f = binarysearch(view(arr, min:mid-1), val; rev=rev, by=by, lt=lt)
            l = binarysearch(view(arr, mid+1:max), val; rev=rev, by=by, lt=lt)
            return (length(f) > 0 ? f.start + min - 1 : mid):(length(l) > 0 ? l.stop + mid : mid)
        end

        ifelse(lt(midv, val), () -> min = mid + 1, () -> max = mid - 1)()
    end

    return mid:mid-1
end