#if !defined(PANGRAM_H)
#define PANGRAM_H

#include <string>
#include <unordered_set>

namespace pangram {
bool is_pangram(std::string str) {
    std::unordered_set<char> counter;

    for (auto &ch : str) {
        if (isalpha(ch)) {
            counter.insert(isupper(ch) ? ch + 32 : ch);
        }
    }

    return counter.size() == 26;
}
}  // namespace pangram

#endif  // PANGRAM_H