dirs = [(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)]
function annotate(field)
    for r in 1:length(field)
        row = collect(field[r])

        for c in 1:length(row)
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