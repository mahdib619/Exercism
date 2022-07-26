dirs = [(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)]
function annotate(field)
    for r in eachindex(field)
        row = collect(field[r])

        for c in eachindex(row)
            row[c] == '*' && continue

            minecount = 0
            for (x, y) in dirs
                cf, rf = c + x, r + y
                (!isassigned(field, rf) || !isassigned(row, cf)) && continue
                minecount += field[rf][cf] == '*' ? 1 : 0
            end

            row[c] = minecount > 0 ? '0' + minecount : ' '
        end

        field[r] = join(row)
    end

    return field
end