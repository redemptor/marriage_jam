using System.Collections.Generic;
using UnityEngine;

public class Azeitonator : Enemy
{
    private float ATTACK_RECOIL_MAX_TIME = 0.5f;
    private float MAX_TIME_CHANGE_ATTACK = 2;
    private float MAX_TIME_RANGED_ATTACK = 4;

    public Vector3 DistanceRanged;
    public AzeitonatorBall AzeitonatorBallObj;
    private IaAttackRangedActor ActorApproach;
    private IaRangedFollowActor _iaRangedFollowActor;
    private IaFollowActor _iaFollowActor;
    private float AttackRecoilCount = 0;
    private bool playingSfxWalk = false;

    private bool DoRangedAttack = false;
    private float timeChangeAttack = 0;

    public AudioClip SfxWalk;
    public AudioClip SfxShoot;
    public AudioClip SfxGetHit;
    public AudioClip SfxGetHitAzeitona;
    public List<string> tagTarget;

    public override void Start()
    {
        base.Start();

        _iaRangedFollowActor = new IaRangedFollowActor(this, DistanceRanged);
        _iaFollowActor = new IaFollowActor(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        TryChangeAttackMethod();

        if (!IsKnockOut && !Stunned && !attacking && !waiting && Alive)
        {
            if (!attacking)
            {
                if (DoRangedAttack)
                {
                    _iaRangedFollowActor.FixedUpdate();
                    transform.Find("ActorApproach").gameObject.SetActive(true);
                }
                else
                {
                    _iaFollowActor.FixedUpdate();
                    transform.Find("ActorApproach").gameObject.SetActive(false);
                }

                if (!playingSfxWalk)
                {
                    PlaySoundsFX(SfxWalk, true);
                    playingSfxWalk = true;
                }
            }
        }

        if (!attacking)
        {
            comboHit = 0;
        }
    }

    public void TryChangeAttackMethod()
    {
        if (!attacking)
        {
            if (timeChangeAttack == 0)
            {
                if (DoRangedAttack)
                {
                    timeChangeAttack = Time.time + MAX_TIME_RANGED_ATTACK;
                }
                else
                {
                    timeChangeAttack = Time.time + MAX_TIME_CHANGE_ATTACK;
                }
            }
            if (Time.time > timeChangeAttack)
            {
                DoRangedAttack = !DoRangedAttack;
                _iaFollowActor.ForceRandomMove(false);
                timeChangeAttack = 0;
            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (Time.time > timeNextHit)
        { SetHit(false); }

        //  AnimateOpacity(currenteAlpha);
        AttackRecoilControll();
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
        animator.SetInteger(ANIM_STATE.ATTACK.ToString(), comboHit);
    }

    public override void Attack()
    {
        if (!Alive || IsKnockOut || Stunned) { return; }

        StopSoundsFX(SfxWalk);
        playingSfxWalk = false;

        if (comboHit == 1)
        {
            return;
        }

        if (!attacking && comboHit == 0)
        {
            comboHit = 1;
            PlaySoundsFX(SfxShoot, false);
            CurrentDamage = DamageNormal;

            timeNextAttack = Time.time + hitDurations[0];
            timeNextHit = timeNextAttack;

            AzeitonatorBallObj.facingRight = facingRight;
            AzeitonatorBallObj.damage = DamageNormal;
            Instantiate(AzeitonatorBallObj, transform.position, Quaternion.identity);
        }
        base.Attack();
    }

    private void AttackRecoilControll()
    {
        if (attacking)
        {
            //Animation finished
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                  && !animator.IsInTransition(0)
                 )
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit 1"))
                {
                    if (comboHit == 1) comboHit = 0;
                }

                if (AttackRecoilCount == 0)
                {
                    AttackRecoilCount = Time.time + ATTACK_RECOIL_MAX_TIME;
                }
                if (Time.time > AttackRecoilCount)
                {
                    AttackRecoilCount = 0;
                    StopAttack();
                }
            }
        }
    }

    public override void SetDamage(Damage damage)
    {
        if (damage.Name.Equals("shoot"))
        {
            PlaySoundsFX(SfxGetHitAzeitona, false);
            playingSfxWalk = false;
            base.SetDamage(damage);
        }
        else
        {
            PlaySoundsFX(SfxGetHit, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            _iaFollowActor.ForceRandomMove(true);
            playingSfxWalk = false;
            PlaySoundsFX(SfxGetHit, false);
        }
    }

}
