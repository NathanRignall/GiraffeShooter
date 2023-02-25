using System;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    public class Bot : Component
    {
        
        enum State
        {
            Idle,
            Patrol,
            Chase,
            Attack,
            // Flee
        }
        
        private State _state;
        private Entity _target;

        public Bot()
        {
            _state = State.Patrol;
            BotSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            // state machine
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Patrol:

                    // get the inventory
                    var inventory = entity.GetComponent<Inventory>();

                    var hasWeapon = false;
                    var hasAmmunition = false;
                    
                    // loop through all items
                    foreach (var item in inventory.Items)
                    {
                        // if the item is a weapon
                        if (item.MetaType == MetaType.Weapon)
                        {
                            // set has weapon to true
                            hasWeapon = true;
                        }
                        
                        // if the item is ammunition
                        if (item.MetaType == MetaType.Ammunition)
                        {
                            // set has ammunition to true
                            hasAmmunition = true;
                        }
                    }
                    
                    // if we have a weapon and ammo, look for a player
                    if (hasWeapon && hasAmmunition)
                    {
                        // get the closest player
                        var player = GetClosestEntity(MetaType.Player);
                        
                        // if we have a player
                        if (player != null)
                        {
                            // get the current location
                            var location1 = entity.GetComponent<Physics>().Position;
                    
                            // get the location of the target
                            var targetLocation1 = _target.GetComponent<Physics>().Position;

                            // get the distance
                            var distance1 = Vector3.Distance(location1, targetLocation1);
                            
                            // if the distance is less than 100
                            if (distance1 < 30)
                            {
                                // set the target
                                _target = player;
                            
                                // go to the chase state
                                _state = State.Chase;

                                break;
                            }
                        }
                    }
                    
                    // if we don't have ammunition
                    if (!hasAmmunition)
                    {
                        // get the closest ammo
                        var ammo = GetClosestEntity(MetaType.Ammunition);
                        
                        // if we have ammo
                        if (ammo != null)
                        {
                            // set the target
                            _target = ammo;
                            
                            // go to the chase state
                            _state = State.Chase;
                        }
                    }
                    
                    // if we don't have a weapon
                    if (!hasWeapon)
                    {
                        // get the closest weapon
                        var weapon = GetClosestEntity(MetaType.Weapon);
                        
                        // if we have a weapon
                        if (weapon != null)
                        {
                            // set the target
                            _target = weapon;
                            
                            // go to the chase state
                            _state = State.Chase;
                        }
                    }

                    break;
                case State.Chase:
                    
                    // don't chase if deleted
                    if (_target == null || _target.IsDeleted)
                    {
                        // go to the patrol state
                        _state = State.Patrol;
                        
                        break;
                    }

                    // get the current location
                    var location2 = entity.GetComponent<Physics>().Position;
                    
                    // get the location of the target
                    var targetLocation2 = _target.GetComponent<Physics>().Position;
                    
                    // get the direction
                    var direction2 = targetLocation2 - location2;
                    
                    // get the angle using x and y
                    var angle2 = (float) Math.Atan2(direction2.Y, direction2.X);


                    // move towards the target
                    var control2 = entity.GetComponent<Control>();
                    
                    control2.Move(angle2, 0.7f);


                    // only check attack if player
                    if (_target.Meta.MetaType == MetaType.Player)
                    {
                        
                        // if we are close enough, attack
                        var distance2 = Vector3.Distance(location2, targetLocation2);
                    
                        if (distance2 < 10)
                        {
                            // go to the attack state
                            _state = State.Attack;
                        }

                        // if we are too far, go back to patrol
                        if (distance2 > 30)
                        {
                            // go to the patrol state
                            _state = State.Patrol;
                        }
                    }

                    break;
                case State.Attack:
                    
                    // get the inventory
                    var inventory3 = entity.GetComponent<Inventory>();

                    var hasWeapon3 = false;
                    var hasAmmunition3 = false;
                    
                    // loop through all items
                    foreach (var item in inventory3.Items)
                    {
                        // if the item is a weapon
                        if (item.MetaType == MetaType.Weapon)
                        {
                            // set has weapon to true
                            hasWeapon3 = true;
                            
                            // select the weapon
                            inventory3.SelectItem(item);
                        }
                        
                        // if the item is ammunition
                        if (item.MetaType == MetaType.Ammunition)
                        {
                            // set has ammunition to true
                            hasAmmunition3 = true;
                        }
                    }
                    
                    // get the current location
                    var location3 = entity.GetComponent<Physics>().Position;
                    
                    // get the location of the target
                    var targetLocation3 = _target.GetComponent<Physics>().Position;
                    
                    // get the direction
                    var direction3 = targetLocation3 - location3 + new Vector3(0f,0.7f,0);
                    
                    // get the angle using x and y
                    var angle3 = (float) Math.Atan2(direction3.Y, direction3.X);


                    // move towards the target
                    // var control3 = entity.GetComponent<Control>();
                    //
                    // control3.Move(angle3, 0.5f);
                    
                    // attack
                    var aim3 = entity.GetComponent<Aim>();
                    aim3.Rotation = angle3;
                    
                    var giraffeEntity = entity as Giraffe;
                    giraffeEntity.Shoot(gameTime.TotalGameTime);

                    // if we are too far, go back to patrol
                    var distance3 = Vector3.Distance(location3, targetLocation3);
                    
                    if (distance3 > 10)
                    {
                        // go to the patrol state
                        _state = State.Patrol;
                    }

                    // if we run out of ammo, go back to patrol
                    if (!hasAmmunition3)
                    {
                        // go to the patrol state
                        _state = State.Patrol;
                    }
                    
                    // // if we run out of health, flee
                    // if (entity.GetComponent<Health>().Value <= 10)
                    // {
                    //     // go to the flee state
                    //     _state = State.Flee;
                    // }
                    
                    break;
                // case State.Flee:
                //     
                //     // get the current location
                //     var location4 = entity.GetComponent<Physics>().Position;
                //     
                //     // get the location of the target
                //     var targetLocation4 = _target.GetComponent<Physics>().Position;
                //     
                //     // get the direction
                //     var direction4 = location4 - targetLocation4;
                //     
                //     // get the angle using x and y
                //     var angle4 = (float) Math.Atan2(direction4.Y, direction4.X);
                //     
                //     // move away from the target
                //     var control4 = entity.GetComponent<Control>();
                //     
                //     control4.Move(angle4, 1f);
                //     
                //     // if we are far enough, go back to patrol
                //     
                //     break;
            }
        }
        
        private Entity GetClosestEntity(MetaType type)
        {
            var entities = ContextManager.WorldContext.EntityCollection.GetEntities(type);
            
            // get the physics component
            var physics = entity.GetComponent<Physics>();
            
            // get the position
            var position = physics.Position;
            
            // get the closest entity by looping through all entities
            Entity closestEntity = null;
            var closestDistance = float.MaxValue;
            
            // loop through all entities
            foreach (var entity in entities)
            {
                // if entity is deleted, skip
                if (entity.IsDeleted || entity == this.entity)
                {
                    continue;
                }
                
                // get the physics component
                var entityPhysics = entity.GetComponent<Physics>();
                
                // get the position
                var entityPosition = entityPhysics.Position;
                
                // get the distance
                var distance = Vector3.Distance(position, entityPosition);
                
                // if the distance is less than the closest distance
                if (distance < closestDistance)
                {
                    // set the closest distance
                    closestDistance = distance;
                    
                    // set the closest entity
                    closestEntity = entity;
                }
            }
            
            // return the closest entity
            return closestEntity;
            
        }

        public override void Deregister()
        {
            BotSystem.Deregister(this);
        }
    }
}

