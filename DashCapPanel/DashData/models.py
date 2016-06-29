from django.contrib.auth.models import User
from django.db import models
from django.conf import settings


class Character(models.Model):
    TIEDTOGETHER = "Tiedtogether"
    KICK = "Kick"
    GROW = "Grow"

    SKILL_CHOICES = (
        (TIEDTOGETHER, TIEDTOGETHER), (KICK, KICK), (GROW, GROW)
    )

    owner = models.ForeignKey(User, blank=True, default=None)
    name = models.CharField(max_length=32)
    speed = models.FloatField(default=25)
    stamina = models.FloatField(default=200)
    skill1 = models.CharField(
        max_length=32,
        choices=SKILL_CHOICES,
        default=KICK
    )
    skill2 = models.CharField(
        max_length=32,
        choices=SKILL_CHOICES,
        default=TIEDTOGETHER
    )

    def __str__(self):
        return self.name + " [" + self.speed.__str__() + " Speed:" + self.stamina.__str__() + " Stamina]"

    @classmethod
    def create(cls, owner, name):
        character = cls(owner=owner, name=name)
        return character

    def as_dict(self):
        return {
            "owner": {
                "id": self.owner_id,
                "username": self.owner.username
            },
            "name": self.name,
            "speed": self.speed,
            "stamina": self.stamina,
            "skill1": self.skill1,
            "skill2": self.skill2,
        }



