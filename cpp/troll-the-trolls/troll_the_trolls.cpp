namespace hellmath {

enum class AccountStatus {
    troll,
    guest,
    user,
    mod
};
enum class Action {
    read,
    write,
    remove
};

bool can_interact(AccountStatus poster, AccountStatus viewer) {
    return (poster == AccountStatus::troll) == (viewer == AccountStatus::troll);
}

bool display_post(AccountStatus poster, AccountStatus viewer) {
    return can_interact(poster, viewer);
}
bool permission_check(Action action, AccountStatus account) {
    switch (account) {
        case AccountStatus::mod:
            return true;
        case AccountStatus::guest:
            return action == Action::read;
        case AccountStatus::troll:
        case AccountStatus::user:
            return action != Action::remove;
        default:
            return false;
    }
}
bool valid_player_combination(AccountStatus player1, AccountStatus player2) {
    return player1 != AccountStatus::guest && player2 != AccountStatus::guest && can_interact(player1, player2);
}
bool has_priority(AccountStatus user1, AccountStatus user2) {
    return user1 > user2;
}
}  // namespace hellmath