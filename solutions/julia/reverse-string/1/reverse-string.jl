using Unicode
const TEST_GRAPHEMES = true
myreverse(str) = join(reverse(collect(graphemes(str))))