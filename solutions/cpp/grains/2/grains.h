#if !defined(GRAINS_H)
#define GRAINS_H

namespace grains {
unsigned long long square(int square_number) {
    return 1ULL << (square_number - 1);
}
unsigned long long total() {
    return __UINT64_MAX__;
}
}  // namespace grains

#endif  // GRAINS_H
