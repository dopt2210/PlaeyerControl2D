public interface IDamageAble 
{
    float _maxHP {  get; set; }
    float _currentHP {  get; set; }
    void Damage(float dmg);
    void Died();
}
